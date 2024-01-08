using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectIndustries.Dashboards.App.Products.Collections;
using ProjectIndustries.Dashboards.App.Products.Services;
using ProjectIndustries.Dashboards.WebApi.Foundation.Model;
using ProjectIndustries.Dashboards.WebApi.Foundation.Mvc.Controllers;

namespace ProjectIndustries.Dashboards.WebApi.Products.Controllers
{
  public class GlobalSearchController : SecuredDashboardBoundControllerBase
  {
    private readonly IGlobalSearchProvider _globalSearchProvider;

    public GlobalSearchController(IServiceProvider provider, IGlobalSearchProvider globalSearchProvider)
      : base(provider)
    {
      _globalSearchProvider = globalSearchProvider;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<GlobalSearchPagedList>))]
    public async ValueTask<IActionResult> GetAsync([FromQuery] GlobalSearchPageRequest pageRequest,
      CancellationToken ct)
    {
      var page = await _globalSearchProvider.GetAllAsync(CurrentDashboardId, pageRequest, ct);
      return Ok(page);
    }
  }
}