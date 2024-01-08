using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectIndustries.Dashboards.App.Products.Model;
using ProjectIndustries.Dashboards.App.Products.Services;
using ProjectIndustries.Dashboards.WebApi.Foundation.Model;
using ProjectIndustries.Dashboards.WebApi.Foundation.Mvc.Controllers;
using System;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.Collections;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.Products.Services;
using ProjectIndustries.Dashboards.WebApi.Foundation.Authorization;

namespace ProjectIndustries.Dashboards.WebApi.Products.Controllers
{
  public class ReleasesController : SecuredDashboardBoundControllerBase
  {
    private readonly IReleaseProvider _releaseProvider;
    private readonly IReleaseRepository _releaseRepository;
    private readonly IReleaseService _releaseService;

    public ReleasesController(IServiceProvider provider, IReleaseProvider releaseProvider,
      IReleaseRepository releaseRepository, IReleaseService releaseService)
      : base(provider)
    {
      _releaseProvider = releaseProvider;
      _releaseRepository = releaseRepository;
      _releaseService = releaseService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<IPagedList<ReleaseRowData>>))]
    public async ValueTask<IActionResult> GetPageAsync([FromQuery] ReleasesPageRequest pageRequest,
      CancellationToken ct)
    {
      var page = await _releaseProvider.GetPageAsync(CurrentDashboardId, pageRequest, ct);
      return Ok(page);
    }

    [HttpGet("Active")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<ActiveReleaseInfoData[]>))]
    public async ValueTask<IActionResult> GetActiveListAsync(CancellationToken ct)
    {
      var list = await _releaseProvider.GetActiveListAsync(CurrentDashboardId, ct);
      return Ok(list);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<ReleaseData>))]
    public async ValueTask<IActionResult> GetByIdAsync(long id, CancellationToken ct)
    {
      var data = await _releaseProvider.GetByIdAsync(id, ct);
      if (data == null)
      {
        return NotFound();
      }

      return Ok(data);
    }

    // +authorize dashboard member or admin
    [HttpGet("stock/{password}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<ReleaseStockData>))]
    public async ValueTask<IActionResult> GetStockAsync(string password, CancellationToken ct)
    {
      var release = await _releaseProvider.GetStockByPasswordAsync(password, ct);
      if (release.HasNoValue)
      {
        return NotFound();
      }

      await AppAuthorizationService.AdminOrMemberAsync(release.Value!.DashboardId)
        .OrThrowForbid();

      return Ok(release);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask CreateAsync([FromBody] SaveReleaseCommand cmd, CancellationToken ct)
    {
      await _releaseService.CreateAsync(CurrentDashboardId, cmd, ct);
    }

    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> UpdateAsync(long id, [FromBody] SaveReleaseCommand cmd, CancellationToken ct)
    {
      var release = await _releaseRepository.GetByIdAsync(id, ct);
      if (release == null)
      {
        return NotFound();
      }

      await _releaseService.UpdateAsync(release, cmd, ct);
      return NoContent();
    }

    [HttpPut("{id:long}/Active")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> ToggleActiveAsync(long id, bool isActive, CancellationToken ct)
    {
      var release = await _releaseRepository.GetByIdAsync(id, ct);
      if (release == null)
      {
        return NotFound();
      }

      release.IsActive = isActive;
      _releaseRepository.Update(release);

      return NoContent();
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> RemoveAsync(long id, CancellationToken ct)
    {
      Release? release = await _releaseRepository.GetByIdAsync(id, ct);
      if (release == null)
      {
        return NotFound();
      }

      _releaseRepository.Remove(release);
      return NoContent();
    }
  }
}