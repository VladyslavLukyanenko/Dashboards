using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using ProjectIndustries.Dashboards.App.Products.Model;
using ProjectIndustries.Dashboards.App.Products.Services;
using ProjectIndustries.Dashboards.Core.Collections;
using ProjectIndustries.Dashboards.Core.Identity;
using ProjectIndustries.Dashboards.Core.Identity.Services;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.Products.Services;
using ProjectIndustries.Dashboards.WebApi.Authorization;
using ProjectIndustries.Dashboards.WebApi.Foundation.Authorization;
using ProjectIndustries.Dashboards.WebApi.Foundation.Model;
using ProjectIndustries.Dashboards.WebApi.Foundation.Mvc.Controllers;

namespace ProjectIndustries.Dashboards.WebApi.Products.Controllers
{
  public class LicenseKeysController : SecuredDashboardBoundControllerBase
  {
    private readonly ILicenseKeyRepository _licenseKeyRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILicenseKeyService _licenseKeyService;
    private readonly ILicenseKeyProvider _licenseKeyProvider;

    public LicenseKeysController(IServiceProvider provider, ILicenseKeyService licenseKeyService,
      ILicenseKeyRepository licenseKeyRepository, IUserRepository userRepository,
      ILicenseKeyProvider licenseKeyProvider)
      : base(provider)
    {
      _licenseKeyService = licenseKeyService;
      _licenseKeyRepository = licenseKeyRepository;
      _userRepository = userRepository;
      _licenseKeyProvider = licenseKeyProvider;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<IPagedList<LicenseKeySnapshotData>>))]
    public async ValueTask<IActionResult> GetLicenseKeys([FromQuery] LicenseKeyPageRequest pageRequest,
      CancellationToken ct)
    {
      var licenseKeys = await _licenseKeyProvider.GetPageAsync(CurrentDashboardId, pageRequest, ct);
      return Ok(licenseKeys);
    }

    [HttpGet("{id:long}/Summary")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<LicenseKeySummaryData>))]
    public async ValueTask<IActionResult> GetLicenseKeySummaryAsync(long id, CancellationToken ct)
    {
      var summary = await _licenseKeyProvider.GetSummaryByIdAsync(id, ct);
      return Ok(summary);
    }

    [HttpPatch("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> UpdateLicenseKeyAsync(long id, [FromBody] UpdateLicenseKeyCommand cmd,
      CancellationToken ct)
    {
      LicenseKey? licenseKey = await _licenseKeyRepository.GetByIdAsync(id, ct);
      if (licenseKey == null)
      {
        return NotFound();
      }

      var result = await _licenseKeyService.UpdateAsync(licenseKey, cmd, ct);
      if (result.IsFailure)
      {
        return BadRequest(result.Error.ToApiError());
      }

      return NoContent();
    }

    [HttpPatch("{id:long}/Prolong")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> ProlongLicenseKeyAsync(long id, int daysToAdd, CancellationToken ct)
    {
      LicenseKey? licenseKey = await _licenseKeyRepository.GetByIdAsync(id, ct);
      if (licenseKey == null)
      {
        return NotFound();
      }

      var result = licenseKey.Prolong(Duration.FromDays(daysToAdd));
      if (result.IsFailure)
      {
        return BadRequest(result.Error.ToApiError());
      }

      _licenseKeyRepository.Update(licenseKey);

      return NoContent();
    }

    [HttpGet("Products/{productId:long}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<IList<LicenseKeySnapshotData>>))]
    public async ValueTask<IActionResult> GetAllByProductIdAsync(long productId, CancellationToken ct)
    {
      var licenseKeys = await _licenseKeyProvider.GetAllByProductIdAsync(CurrentUserId, productId, ct);
      return Ok(licenseKeys);
    }

    [HttpGet("Releases/{releaseId:long}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<IPagedList<LicenseKeyShortData>>))]
    public async ValueTask<IActionResult> GetPageByReleaseIdAsync(long releaseId, [FromQuery] PageRequest pageRequest,
      CancellationToken ct)
    {
      var licenseKeys = await _licenseKeyProvider.GetPageByReleaseIdAsync(releaseId, pageRequest, ct);
      return Ok(licenseKeys);
    }

    // +authorize dashboard owner or admin
    [HttpGet("UsedToday")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<int>))]
    [AuthorizePermission(Permissions.LicenseKeysStatsUsedCount)]
    public async ValueTask<IActionResult> GetUsedTodayCountAsync(Offset offset, CancellationToken ct)
    {
      await AppAuthorizationService.AuthorizeCurrentPermissionsAsync(CurrentDashboardId)
        .OrThrowForbid();

      var count = await _licenseKeyProvider.GetUsedTodayCountAsync(CurrentDashboardId, offset, ct);
      return Ok(count);
    }

    // +authorize dashboard owner or admin
    [HttpPatch("{key}/Suspend")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [AuthorizePermission(Permissions.LicenseKeysToggleSuspend)]
    public async ValueTask<IActionResult> SuspendKeyAsync(string key, CancellationToken ct)
    {
      var licenseKey = await _licenseKeyRepository.GetByValueAsync(key, ct);
      if (licenseKey == null)
      {
        return NotFound();
      }


      await AppAuthorizationService.AuthorizeCurrentPermissionsAsync(licenseKey.DashboardId)
        .OrThrowForbid();

      var suspendResult = await _licenseKeyService.SuspendAsync(licenseKey, ct);
      if (suspendResult.IsFailure)
      {
        return BadRequest();
      }

      return NoContent();
    }

    [HttpPost("Generate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask GenerateAsync([FromBody] GenerateLicenseKeysCommand cmd, CancellationToken ct)
    {
      // todo: authorize plan
      await _licenseKeyService.GenerateKeys(cmd, ct);
    }

    // +authorize dashboard owner or admin
    [HttpPatch("Plans/{planId:long}/Suspend")]
    [AuthorizePermission(Permissions.LicenseKeysToggleSuspend)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> SuspendByPlanIdAsync(long planId, CancellationToken ct = default)
    {
      await AppAuthorizationService.AuthorizeCurrentPermissionsAsync(CurrentDashboardId)
        .OrThrowForbid();

      var result = await _licenseKeyService.SuspendAllAsync(planId, ct);
      if (result.IsFailure)
      {
        return BadRequest();
      }

      return NoContent();
    }

    // +authorize dashboard owner or admin
    [HttpPatch("Plans/{planId:long}/Resume")]
    [AuthorizePermission(Permissions.LicenseKeysToggleSuspend)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> ResumeByPlanIdAsync(long planId, CancellationToken ct = default)
    {
      await AppAuthorizationService.AuthorizeCurrentPermissionsAsync(CurrentDashboardId)
        .OrThrowForbid();

      var result = await _licenseKeyService.ResumeAllAsync(planId, ct);
      if (result.IsFailure)
      {
        return BadRequest();
      }

      return NoContent();
    }

    // +authorize dashboard owner or admin
    [HttpPatch("{key}/Resume")]
    [AuthorizePermission(Permissions.LicenseKeysToggleSuspend)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> ResumeKeyAsync(string key, CancellationToken ct)
    {
      var licenseKey = await _licenseKeyRepository.GetByValueAsync(key, ct);
      if (licenseKey == null)
      {
        return NotFound();
      }

      await AppAuthorizationService.AuthorizeCurrentPermissionsAsync(licenseKey.DashboardId)
        .OrThrowForbid();

      var suspendResult = await _licenseKeyService.ResumeAsync(licenseKey, ct);
      if (suspendResult.IsFailure)
      {
        return BadRequest();
      }

      return NoContent();
    }

    // +authorize dashboard owner or admin
    [HttpPatch("{key}/Reset")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> ResetAsync(string key, CancellationToken ct)
    {
      var licenseKey = await _licenseKeyRepository.GetByValueAsync(key, ct);
      if (licenseKey == null)
      {
        return NotFound();
      }

      await AppAuthorizationService.AdminOrSameUserAsync(licenseKey.UserId.GetValueOrDefault())
        .OrThrowForbid();

      licenseKey.ResetSession();
      _licenseKeyRepository.Update(licenseKey);

      return NoContent();
    }

    // +authorize dashboard member or admin
    [HttpPost("{key}/Bind")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<bool>))]
    public async ValueTask<IActionResult> BindToUserAsync(string key, CancellationToken ct = default)
    {
      User? user = await _userRepository.GetByIdAsync(CurrentUserId, ct);
      LicenseKey? licenseKey = await _licenseKeyRepository.GetByValueAsync(key, ct);
      if (licenseKey == null)
      {
        return NotFound();
      }

      await AppAuthorizationService.AdminOrMemberAsync(licenseKey.DashboardId)
        .OrThrowForbid();

      var discordToken = User.FindFirst("discordToken")!.Value;
      var result = await _licenseKeyService.BindAsync(licenseKey, user!, discordToken, ct);
      if (result.IsFailure)
      {
        return BadRequest();
      }

      return Ok(licenseKey.IsLifetime());
    }

    // +authorize key owner or dashboard owner or admin
    [HttpDelete("{key}/Unbind")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<bool>))]
    public async ValueTask<IActionResult> UnbindFromUserAsync(string key, CancellationToken ct)
    {
      LicenseKey? licenseKey = await _licenseKeyRepository.GetByValueAsync(key, ct);
      if (licenseKey == null)
      {
        return NotFound();
      }
      //
      // await AppAuthorizationService.AdminOrSameUserAsync(licenseKey.UserId.GetValueOrDefault())
      //   .OrThrowForbid();

      var unboundResult = await _licenseKeyService.UnbindAsync(licenseKey, ct);
      if (unboundResult.IsFailure)
      {
        return BadRequest();
      }

      return Ok(licenseKey.Value);
    }

    // +authorize key owner or dashboard owner or admin
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<bool>))]
    public async ValueTask<IActionResult> RemoveAsync(long id, CancellationToken ct)
    {
      LicenseKey? licenseKey = await _licenseKeyRepository.GetByIdAsync(id, ct);
      if (licenseKey == null)
      {
        return NotFound();
      }
      //
      // await AppAuthorizationService.AdminOrSameUserAsync(licenseKey.UserId.GetValueOrDefault())
      //   .OrThrowForbid();

      var unboundResult = await _licenseKeyService.RemoveKeyAsync(licenseKey, ct);
      if (unboundResult.IsFailure)
      {
        return BadRequest();
      }

      return Ok(licenseKey.Value);
    }
  }
}