using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.Primitives;

namespace ProjectIndustries.Dashboards.Core.Security.Services
{
  public interface IMemberRoleRepository : ICrudRepository<MemberRole>
  {
    ValueTask<IList<MemberRole>> GetDashboardRolesByIdsAsync(Guid dashboardId, IEnumerable<long> roleIds,
      CancellationToken ct = default);
  }
}