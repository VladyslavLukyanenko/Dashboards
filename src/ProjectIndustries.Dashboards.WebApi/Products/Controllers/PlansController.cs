using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectIndustries.Dashboards.App.Products.Model;
using ProjectIndustries.Dashboards.App.Products.Services;
using ProjectIndustries.Dashboards.WebApi.Foundation.Model;
using ProjectIndustries.Dashboards.WebApi.Foundation.Mvc.Controllers;

namespace ProjectIndustries.Dashboards.WebApi.Products.Controllers
{
  public class PlansController : SecuredDashboardBoundControllerBase
  {
    private readonly IPlanProvider _planProvider;

    public PlansController(IServiceProvider provider, IPlanProvider planProvider) : base(provider)
    {
      _planProvider = planProvider;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<PlanData[]>))]
    public async ValueTask<IActionResult> GetAllAsync(CancellationToken ct)
    {
      var list = await _planProvider.GetAllAsync(CurrentDashboardId, ct);
      return Ok(list);
    }
  }
}