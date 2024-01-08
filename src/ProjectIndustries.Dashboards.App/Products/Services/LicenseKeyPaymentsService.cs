using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ProjectIndustries.Dashboards.Core.Identity;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.Products.Services;

namespace ProjectIndustries.Dashboards.App.Products.Services
{
  public class LicenseKeyPaymentsService : ILicenseKeyPaymentsService
  {
    private readonly IPlanRepository _planRepository;
    private readonly IStripeGateway _stripeGateway;
    private readonly ILicenseKeyService _licenseKeyService;
    private readonly IDiscordService _discordService;
    private readonly ILicenseKeyGenerationStrategyProvider _keyGenerationStrategyProvider;

    public LicenseKeyPaymentsService(IPlanRepository planRepository, IStripeGateway stripeGateway,
      ILicenseKeyService licenseKeyService, IDiscordService discordService,
      ILicenseKeyGenerationStrategyProvider keyGenerationStrategyProvider)
    {
      _planRepository = planRepository;
      _stripeGateway = stripeGateway;
      _licenseKeyService = licenseKeyService;
      _discordService = discordService;
      _keyGenerationStrategyProvider = keyGenerationStrategyProvider;
    }

    public async ValueTask<Result> ProcessPaymentAsync(Dashboard dashboard, long planId, Release release, string intent,
      string customer, User user, string discordToken, CancellationToken ct = default)
    {
      Plan? plan = await _planRepository.GetByIdAsync(planId, ct);
      return await RegisterLicenseKeyForUserAsync(dashboard, user, plan, release, discordToken, intent, customer, ct);
    }

    public async ValueTask<Result> AcquireTrialKeyAsync(Dashboard dashboard, long planId, Release release, User user,
      string discordToken, CancellationToken ct = default)
    {
      Plan? plan = await _planRepository.GetByIdAsync(planId, ct);
      if (plan?.IsTrial == false)
      {
        return Result.Failure("This product plan doesn't support trial keys");
      }

      return await RegisterLicenseKeyForUserAsync(dashboard, user, plan, release, discordToken, ct: ct);
    }

    private async ValueTask<Result> RegisterLicenseKeyForUserAsync(Dashboard dashboard, User user, Plan? plan,
      Release release, string discordToken, string? paymentIntent = null, string? stripeCustomerId = null,
      CancellationToken ct = default)
    {
      if (plan == null)
      {
        return Result.Failure("Plan not found");
      }

      string? subscriptionId = null;
      if (plan.IsLifetimeLimited() && !plan.IsTrial)
      {
        if (string.IsNullOrEmpty(stripeCustomerId))
        {
          return Result.Failure("Can't start subscription with empty stripe customer id");
        }

        var startResult =
          await _stripeGateway.StartSubscriptionAsync(stripeCustomerId, plan, plan.CalculateKeyExpiry(), ct);
        if (startResult.IsFailure)
        {
          return startResult;
        }

        subscriptionId = startResult.Value;
      }

      var strategy = _keyGenerationStrategyProvider.GetGeneratorFor(plan.LicenseKeyConfig);
      var keyValue = await strategy.GenerateValueAsync(plan, ct);
      return await _licenseKeyService.PurchaseAsync(user, plan, release, keyValue, paymentIntent, subscriptionId, ct)
        .AsTask()
        .OnSuccessTry(async _ => await _discordService.JoinToGuildAsync(dashboard, discordToken, user, plan, ct));
    }
  }
}