using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ProjectIndustries.Dashboards.App.Products.Services;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.Products.Services;
using Stripe;

namespace ProjectIndustries.Dashboards.WebApi.Services.Stripe
{
  public class CancelSubscriptionOnCustomerSubscriptionDeletedWebHookHandler : IStripeWebHookHandler
  {
    private readonly ILicenseKeyService _licenseKeyService;

    public CancelSubscriptionOnCustomerSubscriptionDeletedWebHookHandler(ILicenseKeyService licenseKeyService)
    {
      _licenseKeyService = licenseKeyService;
    }

    public bool CanHandle(string eventType) => eventType == Events.CustomerSubscriptionDeleted;

    public async ValueTask<Result> HandleAsync(Event @event, Dashboard dashboard, CancellationToken ct = default)
    {
      var subscription = (Subscription) @event.Data.Object;
      return await _licenseKeyService.CancelSubscriptionAsync(subscription.Id, ct);
    }
  }
}