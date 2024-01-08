using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectIndustries.Dashboards.App.Forms.Model;
using ProjectIndustries.Dashboards.App.Forms.Services;
using ProjectIndustries.Dashboards.Core.Forms;
using ProjectIndustries.Dashboards.Core.Forms.Services;
using ProjectIndustries.Dashboards.WebApi.Foundation.Authorization;
using ProjectIndustries.Dashboards.WebApi.Foundation.Model;
using ProjectIndustries.Dashboards.WebApi.Foundation.Mvc.Controllers;

namespace ProjectIndustries.Dashboards.WebApi.Forms.Controllers
{
  public class FormsController : SecuredDashboardBoundControllerBase
  {
    private readonly IFormRepository _formRepository;
    private readonly IFormProvider _formProvider;

    public FormsController(IServiceProvider provider, IFormRepository formRepository, IFormProvider formProvider)
      : base(provider)
    {
      _formRepository = formRepository;
      _formProvider = formProvider;
    }

    [HttpGet("{formId:long}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<FormData>))]
    public async ValueTask<IActionResult> GetAsync(long formId, CancellationToken ct)
    {
      FormData? data = await _formProvider.GetUserFormAsync(formId, CurrentUserId, ct);
      if (data == null)
      {
        return NotFound();
      }

      await AppAuthorizationService.AdminOrMemberAsync(data.DashboardId)
        .OrThrowForbid();

      return Ok(data);
    }

    [HttpDelete("{formId:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> RemoveAsync(long formId, CancellationToken ct)
    {
      Form? form = await _formRepository.GetByIdAsync(formId, ct);
      if (form == null)
      {
        return NotFound();
      }

      _formRepository.Remove(form);
      return NoContent();
    }
  }
}