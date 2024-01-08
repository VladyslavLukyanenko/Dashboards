using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.App.Security.Model;

namespace ProjectIndustries.Dashboards.App.Security.Services
{
  public interface IMemberRoleProvider
  {
    ValueTask<IList<MemberRoleData>> GetMemberRolesAsync(Guid dashboardId, CancellationToken ct = default);
    ValueTask<IList<BoundMemberRoleData>> GetRolesAsync(Guid dashboardId, long userId, CancellationToken ct = default);
  }
}