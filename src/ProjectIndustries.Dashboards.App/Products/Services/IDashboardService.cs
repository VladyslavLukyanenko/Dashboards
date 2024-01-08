using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.Products;

namespace ProjectIndustries.Dashboards.App.Products.Services
{
  public interface IDashboardService
  {
    ValueTask UpdateAsync(Dashboard dashboard, UpdateDashboardCommand cmd, CancellationToken ct = default);
  }
}