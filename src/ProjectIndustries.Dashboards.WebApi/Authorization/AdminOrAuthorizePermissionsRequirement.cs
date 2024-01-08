using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace ProjectIndustries.Dashboards.WebApi.Authorization
{
  public class AdminOrAuthorizePermissionsRequirement : IAuthorizationRequirement
  {
    public AdminOrAuthorizePermissionsRequirement(IReadOnlyList<string> requiredPermissions, Guid dashboardId)
    {
      RequiredPermissions = requiredPermissions;
      DashboardId = dashboardId;
    }

    public IReadOnlyList<string> RequiredPermissions { get; }
    public Guid DashboardId { get; }
  }
}