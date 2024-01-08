using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ProjectIndustries.Dashboards.WebApi.Authorization
{
  public interface IAppAuthorizationService
  {
    ValueTask<AuthorizationResult> AdminOrMemberAsync(Guid dashboardId);
    ValueTask<AuthorizationResult> AdminOrSameUserAsync(long userId);
    // ValueTask<AuthorizationResult> AdminOrDashboardOwnerAsync(Guid dashboardId);
    ValueTask<AuthorizationResult> AuthorizeCurrentPermissionsAsync(Guid dashboardId);
  }
}