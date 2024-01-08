using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ProjectIndustries.Dashboards.WebApi.Foundation.Authorization;

namespace ProjectIndustries.Dashboards.WebApi.Authorization
{
  public class AdminOrDashboardOwnerAuthorizationHandler : AuthorizationHandler<AdminOrDashboardOwnerRequirement>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
      AdminOrDashboardOwnerRequirement requirement)
    {
      if (context.User.HasAdminRole() || context.User.OwnsDashboard(requirement.DashboardId))
      {
        context.Succeed(requirement);
      }
      
      return Task.CompletedTask;
    }
  }
}