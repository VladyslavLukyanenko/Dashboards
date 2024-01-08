using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.App.Products.Model;
using ProjectIndustries.Dashboards.App.Products.Services;
using ProjectIndustries.Dashboards.Core.Collections;

namespace ProjectIndustries.Dashboards.App.Security.Services
{
  public interface IMemberRoleBindingProvider
  {
    ValueTask<IPagedList<StaffMemberData>> GetMembersPageAsync(Guid dashboardId, StaffMemberPageRequest pageRequest,
      CancellationToken ct);

    ValueTask<MemberSummaryData?> GetSummaryAsync(long userId, Guid dashboardId, CancellationToken ct = default);
    ValueTask<IList<StaffRoleMembersData>> GetRolesAsync(Guid dashboardId, CancellationToken ct = default);
  }
}