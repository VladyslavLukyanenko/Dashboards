using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using ProjectIndustries.Dashboards.Core.Security;
using ProjectIndustries.Dashboards.Core.Security.Services;

namespace ProjectIndustries.Dashboards.WebApi.Authorization
{
  public class AppAuthorizationService : IAppAuthorizationService
  {
    private readonly IAuthorizationService _authorizationService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPermissionsAccessor _permissionsAccessor;

    public AppAuthorizationService(IAuthorizationService authorizationService,
      IHttpContextAccessor httpContextAccessor, IPermissionsAccessor permissionsAccessor)
    {
      _authorizationService = authorizationService;
      _httpContextAccessor = httpContextAccessor;
      _permissionsAccessor = permissionsAccessor;
    }

    public async ValueTask<AuthorizationResult> AdminOrMemberAsync(Guid dashboardId) =>
      await AuthorizeAsync(new AdminOrMemberRequirement(dashboardId));

    public async ValueTask<AuthorizationResult> AdminOrSameUserAsync(long userId) =>
      await AuthorizeAsync(new AdminOrSameUserRequirement(userId));

    public async ValueTask<AuthorizationResult> AdminOrDashboardOwnerAsync(Guid dashboardId) =>
      await AuthorizeAsync(new AdminOrDashboardOwnerRequirement(dashboardId));

    public async ValueTask<AuthorizationResult> AuthorizeCurrentPermissionsAsync(Guid dashboardId) =>
      await AuthorizeAsync(
        new AdminOrAuthorizePermissionsRequirement(_permissionsAccessor.CurrentRequiredPermissions, dashboardId));

    private async ValueTask<AuthorizationResult> AuthorizeAsync(IAuthorizationRequirement requirement) =>
      await _authorizationService.AuthorizeAsync(User, User, requirement);

    private ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User
                                    ?? throw new InvalidOperationException("Requires http context to work");
  }
}