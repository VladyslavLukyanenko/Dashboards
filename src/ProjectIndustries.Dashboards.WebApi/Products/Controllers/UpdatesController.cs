using Microsoft.AspNetCore.Mvc;
using ProjectIndustries.Dashboards.App.Products.Services;
using ProjectIndustries.Dashboards.WebApi.Foundation.Mvc.Controllers;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace ProjectIndustries.Dashboards.WebApi.Products.Controllers
{
  public class UpdatesController : SecuredDashboardBoundControllerBase
  {
    private readonly IUpdatesService _updatesService;

    public UpdatesController(IServiceProvider provider, IUpdatesService updatesService) 
      : base(provider)
    {
      _updatesService = updatesService;
    }

    [HttpGet("{dashboardId:guid}/{product}/{chanel}/{os}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Version))]
    [AllowAnonymous]
    public IActionResult GetLatestVersion(Guid dashboardId, string product, string channel, string os)
    {
      var version = _updatesService.GetLatestVersion(dashboardId, product, channel, os);
      if (version.HasNoValue)
      {
        return NotFound();
      }

      return Ok(version.Value!);
    }
  }
}
