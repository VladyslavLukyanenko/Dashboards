using System;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.Products;
using Stripe;

namespace ProjectIndustries.Dashboards.Infra.Services
{
  public interface IStripeClientFactory
  {
    ValueTask<IStripeClient> CreateClientAsync(Guid dashboardId, CancellationToken ct = default);
    IStripeClient CreateClient(Dashboard dashboard);
  }
}