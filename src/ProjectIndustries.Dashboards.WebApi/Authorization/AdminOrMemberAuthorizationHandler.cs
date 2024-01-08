using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ProjectIndustries.Dashboards.WebApi.Foundation.Authorization;

namespace ProjectIndustries.Dashboards.WebApi.Authorization
{
  public class AdminOrMemberAuthorizationHandler : AuthorizationHandler<AdminOrMemberRequirement>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
      AdminOrMemberRequirement requirement)
    {
      if (context.User.HasAdminRole() || context.User.OwnsDashboard(requirement.DashboardId)
                                      || context.User.JoinedDashboard(requirement.DashboardId))
      {
        context.Succeed(requirement);
      }

      return Task.CompletedTask;
    }
  }
}