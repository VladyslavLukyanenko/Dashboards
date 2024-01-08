using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ProjectIndustries.Dashboards.App.Products.Services;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.Products.Services;
using Stripe;

namespace ProjectIndustries.Dashboards.WebApi.Services.Stripe
{
  public class TryResumeSubscriptionOnCustomerSubscriptionUpdatedWebHookHandler : IStripeWebHookHandler
  {
    private readonly ILicenseKeyService _licenseKeyService;

    public TryResumeSubscriptionOnCustomerSubscriptionUpdatedWebHookHandler(ILicenseKeyService licenseKeyService)
    {
      _licenseKeyService = licenseKeyService;
    }

    public bool CanHandle(string eventType) => eventType == Events.CustomerSubscriptionUpdated;

    public async ValueTask<Result> HandleAsync(Event @event, Dashboard dashboard, CancellationToken ct = default)
    {
      var subscription = (Subscription) @event.Data.Object;
      if (!subscription.CancelAtPeriodEnd)
      {
        await _licenseKeyService.TryResumeSubscriptionAsync(subscription.Id, ct);
      }

      return Result.Success();
    }
  }
}