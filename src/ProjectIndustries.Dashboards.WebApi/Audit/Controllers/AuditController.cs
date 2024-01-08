using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectIndustries.Dashboards.App.Audit.Data;
using ProjectIndustries.Dashboards.App.Audit.Services;
using ProjectIndustries.Dashboards.Core.Collections;
using ProjectIndustries.Dashboards.WebApi.Foundation.Model;
using ProjectIndustries.Dashboards.WebApi.Foundation.Mvc.Controllers;

namespace ProjectIndustries.Dashboards.WebApi.Audit.Controllers
{
  // [Authorize] admin
  public class AuditController : SecuredControllerBase
  {
    private readonly IChangeSetProvider _changeSetProvider;

    public AuditController(IServiceProvider provider, IChangeSetProvider changeSetProvider)
      : base(provider)
    {
      _changeSetProvider = changeSetProvider;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiContract<IPagedList<ChangeSetData>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetChangesPageAsync([FromQuery] ChangeSetPageRequest pageRequest,
      CancellationToken ct = default)
    {
      var page = await _changeSetProvider.GetChangeSetPageAsync(pageRequest, ct);
      return Ok(page);
    }

    [HttpGet("{entryId:guid}")]
    [ProducesResponseType(typeof(ApiContract<ChangesetEntryPayloadData>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetChangeSetEntryPayloadPageAsync(Guid entryId)
    {
      var payload = await _changeSetProvider.GetPayloadAsync(entryId);
      if (payload == null)
      {
        return NotFound();
      }

      return Ok(payload);
    }
  }
}