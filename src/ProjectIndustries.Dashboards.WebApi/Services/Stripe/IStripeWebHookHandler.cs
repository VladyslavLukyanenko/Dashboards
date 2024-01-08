using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ProjectIndustries.Dashboards.Core;
using ProjectIndustries.Dashboards.Core.Products;
using Stripe;

namespace ProjectIndustries.Dashboards.WebApi.Services.Stripe
{
  public interface IStripeWebHookHandler
  {
    bool CanHandle(string eventType);
    ValueTask<Result> HandleAsync(Event @event, Dashboard dashboard, CancellationToken ct = default);
  }
}