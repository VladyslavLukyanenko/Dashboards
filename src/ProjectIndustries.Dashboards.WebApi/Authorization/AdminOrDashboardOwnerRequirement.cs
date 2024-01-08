using System;
using Microsoft.AspNetCore.Authorization;

namespace ProjectIndustries.Dashboards.WebApi.Authorization
{
  public class AdminOrDashboardOwnerRequirement : IAuthorizationRequirement
  {
    public AdminOrDashboardOwnerRequirement(Guid dashboardId)
    {
      DashboardId = dashboardId;
    }

    public Guid DashboardId { get; }
  }
}