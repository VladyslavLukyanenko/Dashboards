using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.App.Products.Model;

namespace ProjectIndustries.Dashboards.App.Products.Services
{
  public interface IDashboardRefProvider
  {
    ValueTask<DashboardRef> GetRefAsync(Guid dashboardId, CancellationToken ct = default);

    ValueTask<IDictionary<Guid, DashboardRef>> GetRefsAsync(IEnumerable<Guid> dashboardIds,
      CancellationToken ct = default);
  }
}