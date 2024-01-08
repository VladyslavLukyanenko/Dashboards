using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectIndustries.Dashboards.App.Security.Model;
using ProjectIndustries.Dashboards.App.Security.Services;
using ProjectIndustries.Dashboards.WebApi.Foundation.Model;
using ProjectIndustries.Dashboards.WebApi.Foundation.Mvc.Controllers;

namespace ProjectIndustries.Dashboards.WebApi.Security.Controllers
{
  public class PermissionsController : SecuredDashboardBoundControllerBase
  {
    private readonly IPermissionProvider _permissions;

    public PermissionsController(IServiceProvider provider, IPermissionProvider permissions)
      : base(provider)
    {
      _permissions = permissions;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<PermissionInfoData[]>))]
    public IActionResult GetUsedPermissions()
    {
      return Ok(_permissions.GetSupportedPermissions());
    }
  }
}