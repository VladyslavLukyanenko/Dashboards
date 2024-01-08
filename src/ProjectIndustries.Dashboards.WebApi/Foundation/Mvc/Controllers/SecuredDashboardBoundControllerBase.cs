using System;
using Microsoft.Extensions.DependencyInjection;
using ProjectIndustries.Dashboards.WebApi.Authorization;
using ProjectIndustries.Dashboards.WebApi.Foundation.Authorization;

namespace ProjectIndustries.Dashboards.WebApi.Foundation.Mvc.Controllers
{
  public abstract class SecuredDashboardBoundControllerBase : SecuredControllerBase
  {
    private Guid? _currentDashboardId;
    protected readonly IAppAuthorizationService AppAuthorizationService;

    protected SecuredDashboardBoundControllerBase(IServiceProvider provider)
      : base(provider)
    {
      AppAuthorizationService = provider.GetRequiredService<IAppAuthorizationService>();
    }

    protected Guid CurrentDashboardId => _currentDashboardId ??= User.GetDashboardId()!.Value;
  }
}