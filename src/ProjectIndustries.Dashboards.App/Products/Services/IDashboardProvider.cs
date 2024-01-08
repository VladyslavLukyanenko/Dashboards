using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.App.Products.Model;
using ProjectIndustries.Dashboards.Core.Products;

namespace ProjectIndustries.Dashboards.App.Products.Services
{
  public interface IDashboardProvider
  {
    ValueTask<DashboardData?> GetByOwnerIdAsync(long ownerId, CancellationToken ct = default);
    ValueTask<DashboardLoginData?> GetLoginDataAsync(IEnumerable<KeyValuePair<DashboardHostingMode, string>> modes,
      CancellationToken ct = default);
  }
}