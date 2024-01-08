using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ProjectIndustries.Dashboards.WebApi.Foundation.Authorization;

namespace ProjectIndustries.Dashboards.WebApi.Authorization
{
  public class AdminOrAuthorizePermissionsAuthorizationHandler
    : AuthorizationHandler<AdminOrAuthorizePermissionsRequirement>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
      AdminOrAuthorizePermissionsRequirement requirement)
    {
      var permissions = context.User.GetPermissions();
      if (context.User.HasAdminRole() || context.User.OwnsDashboard(requirement.DashboardId)
                                      || requirement.RequiredPermissions.All(p => permissions.Contains(p)))
      {
        context.Succeed(requirement);
      }

      return Task.CompletedTask;
    }
  }
}