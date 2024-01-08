using System;
using Microsoft.AspNetCore.Authorization;

namespace ProjectIndustries.Dashboards.WebApi.Authorization
{
  public class AdminOrMemberRequirement : IAuthorizationRequirement
  {
    public AdminOrMemberRequirement(Guid dashboardId)
    {
      DashboardId = dashboardId;
    }

    public Guid DashboardId { get; }
  }
}