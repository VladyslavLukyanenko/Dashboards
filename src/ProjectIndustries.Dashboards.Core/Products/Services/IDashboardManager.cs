using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectIndustries.Dashboards.Core.Products.Services
{
  public interface IDashboardManager
  {
    ValueTask<bool> TryJoinAsync(Guid dashboardId, long userId, CancellationToken ct = default);
  }
}