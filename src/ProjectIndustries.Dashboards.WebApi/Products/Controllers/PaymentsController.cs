using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectIndustries.Dashboards.App.Products.Services;
using ProjectIndustries.Dashboards.Core.Identity;
using ProjectIndustries.Dashboards.Core.Identity.Services;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.Products.Services;
using ProjectIndustries.Dashboards.WebApi.Foundation.Authorization;
using ProjectIndustries.Dashboards.WebApi.Foundation.Model;
using ProjectIndustries.Dashboards.WebApi.Foundation.Mvc.Controllers;
using ProjectIndustries.Dashboards.WebApi.Products.Commands;

namespace ProjectIndustries.Dashboards.WebApi.Products.Controllers
{
  public class PaymentsController : SecuredDashboardBoundControllerBase
  {
    private readonly IStripeGateway _stripeGateway;
    private readonly ILicenseKeyPaymentsService _licenseKeyPaymentsService;
    private readonly IDashboardRepository _dashboardRepository;
    private readonly IReleaseRepository _releaseRepository;
    private readonly IUserRepository _userRepository;

    public PaymentsController(IServiceProvider provider, IStripeGateway stripeGateway,
      ILicenseKeyPaymentsService licenseKeyPaymentsService, IReleaseRepository releaseRepository,
      IDashboardRepository dashboardRepository,
      IUserRepository userRepository)
      : base(provider)
    {
      _stripeGateway = stripeGateway;
      _licenseKeyPaymentsService = licenseKeyPaymentsService;
      _releaseRepository = releaseRepository;
      _dashboardRepository = dashboardRepository;
      _userRepository = userRepository;
    }

    [HttpGet("Panel")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<string>))]
    public async ValueTask<IActionResult> GetBillingPortalLinkAsync(CancellationToken ct)
    {
      string? stripeCustomerId = User.GetStripeCustomerId();
      if (string.IsNullOrEmpty(stripeCustomerId))
      {
        return NotFound();
      }

      var dashboard = await _dashboardRepository.GetByIdAsync(CurrentDashboardId, ct);
      var result = await _stripeGateway.OpenBillingPortalSessionAsync(stripeCustomerId, dashboard!, ct);
      if (result.IsFailure)
      {
        return BadRequest();
      }

      return Ok(result.Value);
    }

    // +authorize dashboard member or admin
    [HttpPost]
    public async ValueTask<IActionResult> CreatePaymentAsync([FromBody] CreatePaymentCommand cmd, CancellationToken ct)
    {
      string? stripeCustomerId = User.GetStripeCustomerId();
      if (string.IsNullOrEmpty(stripeCustomerId))
      {
        return NotFound();
      }

      Release? drop = await _releaseRepository.GetValidByPasswordAsync(cmd.Password, ct);
      if (drop == null)
      {
        return NotFound();
      }

      await AppAuthorizationService.AdminOrMemberAsync(drop.DashboardId)
        .OrThrowForbid();

      var dashboard = await _dashboardRepository.GetByIdAsync(CurrentDashboardId, ct);
      var r = await _stripeGateway.CreatePaymentSessionAsync(drop, dashboard!, stripeCustomerId, ct);
      return r.IsFailure
        ? (IActionResult) BadRequest()
        : Ok(r.Value);
    }

    [HttpPost("{sessionId}/Process")]
    public async ValueTask<IActionResult> ProcessPaymentAsync(string sessionId, CancellationToken ct)
    {
      var result = await _stripeGateway.GetSessionDataAsync(CurrentDashboardId, sessionId, ct);
      if (result.IsFailure)
      {
        return BadRequest();
      }

      var data = result.Value;
      var decrementResult = await GetDecrementedReleaseAsync(data.Password, ct);
      if (decrementResult.IsFailure)
      {
        return BadRequest();
      }

      Result<bool> isCapturedResult = await _stripeGateway.IsCapturedAsync(CurrentDashboardId, data.Intent, ct);
      if (isCapturedResult.IsFailure || !isCapturedResult.Value)
      {
        return BadRequest();
      }

      User? user = await _userRepository.GetByIdAsync(CurrentUserId, ct);
      if (user == null)
      {
        return BadRequest();
      }

      var discordToken = User.GetDiscordAccessToken();
      var dashboard = await _dashboardRepository.GetByIdAsync(CurrentDashboardId, ct);
      var processingResult = await _licenseKeyPaymentsService.ProcessPaymentAsync(dashboard!, data.PlanId,
        decrementResult.Value, data.Intent, data.Customer, user, discordToken!, ct);

      return processingResult.IsFailure
        ? (IActionResult) BadRequest()
        : NoContent();
    }


    [HttpDelete("{sessionId}")]
    public async ValueTask CancelPaymentAsync(string sessionId, CancellationToken ct)
    {
      await _stripeGateway.CancelSubscriptionAsync(CurrentDashboardId, sessionId, ct);
    }

    // authorize dashboard member or admin
    [HttpPost("{password}/Trial")]
    public async ValueTask<IActionResult> ObtainTrialAsync(string password, CancellationToken ct)
    {
      var decrementResult = await GetDecrementedReleaseAsync(password, ct);
      if (decrementResult.IsFailure)
      {
        return BadRequest();
      }

      User? user = await _userRepository.GetByIdAsync(CurrentUserId, ct);
      if (user == null)
      {
        return BadRequest();
      }

      var discordToken = User.GetDiscordAccessToken();
      var dashboard = await _dashboardRepository.GetByIdAsync(CurrentDashboardId, ct);
      var obtainResult = await _licenseKeyPaymentsService.AcquireTrialKeyAsync(dashboard!, decrementResult.Value.PlanId,
        decrementResult.Value, user, discordToken!, ct);

      return obtainResult.IsFailure
        ? (IActionResult) BadRequest()
        : NoContent();
    }

    private async ValueTask<Result<Release>> GetDecrementedReleaseAsync(string password, CancellationToken ct)
    {
      Release? release = await _releaseRepository.GetValidByPasswordAsync(password, ct);
      if (release == null)
      {
        return Result.Failure<Release>("Can't find drop");
      }

      await AppAuthorizationService.AdminOrMemberAsync(release.DashboardId)
        .OrThrowForbid();

      Result decrementResult = await _releaseRepository.DecrementAsync(release, ct);
      if (decrementResult.IsFailure)
      {
        return Result.Failure<Release>("No available item to buy");
      }

      return release;
    }
  }
}