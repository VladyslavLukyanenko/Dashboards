using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.Primitives;

namespace ProjectIndustries.Dashboards.Core.Products.Services
{
  public interface IDashboardRepository : ICrudRepository<Dashboard, Guid>
  {
    ValueTask<Dashboard?> GetByRawLocationAsync(string rawLocation, CancellationToken ct = default);

    ValueTask<bool> AlreadyJoinedAsync(Guid dashboardId, long userId, CancellationToken ct = default);
    ValueTask<JoinedDashboard> CreateJoinAsync(JoinedDashboard joinedDashboard, CancellationToken ct = default);
    ValueTask<IList<AccessibleDashboard>> GetAccessibleDashboardsAsync(long userId, CancellationToken ct = default);
    ValueTask<Dashboard?> GetByOwnerIdAsync(long userId, CancellationToken ct = default);
  }
}