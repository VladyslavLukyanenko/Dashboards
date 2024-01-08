using System;
using Microsoft.AspNetCore.Authorization;
using ProjectIndustries.Dashboards.WebApi.Foundation.Authorization;

namespace ProjectIndustries.Dashboards.WebApi.Foundation.Mvc.Controllers
{
  [Authorize(Policy = "SoftwareClientsOnly", AuthenticationSchemes = "LicenseKey")]
  public abstract class SoftwareDashboardBoundControllerBase : SecuredControllerBase
  {
    private string? _currentLicenseKey;

    protected SoftwareDashboardBoundControllerBase(IServiceProvider provider)
      : base(provider)
    {
    }

    protected string CurrentLicenseKey => _currentLicenseKey ??= User.GetLicenseKey()!;
  }
}