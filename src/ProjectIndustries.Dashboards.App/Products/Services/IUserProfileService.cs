using System;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.Products;

namespace ProjectIndustries.Dashboards.App.Products.Services
{
  public interface IUserProfileService
  {
    ValueTask RefreshUserProfileIfOutdatedAsync(long userId, Guid dashboardId, CancellationToken ct = default);
    ValueTask RefreshUserProfileIfOutdatedAsync(long userId, Dashboard dashboard, CancellationToken ct = default);
  }
}