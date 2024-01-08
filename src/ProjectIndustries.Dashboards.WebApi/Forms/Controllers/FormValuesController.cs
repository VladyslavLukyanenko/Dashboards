using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectIndustries.Dashboards.App.Forms.Services;
using ProjectIndustries.Dashboards.Core.Forms;
using ProjectIndustries.Dashboards.Core.Forms.Services;
using ProjectIndustries.Dashboards.WebApi.Foundation.Authorization;
using ProjectIndustries.Dashboards.WebApi.Foundation.Mvc.Controllers;

namespace ProjectIndustries.Dashboards.WebApi.Forms.Controllers
{
  public class FormValuesController : SecuredDashboardBoundControllerBase
  {
    private readonly IFormValueService _formValueService;
    private readonly IFormRepository _formRepository;

    public FormValuesController(IServiceProvider provider, IFormValueService formValueService,
      IFormRepository formRepository) : base(provider)
    {
      _formValueService = formValueService;
      _formRepository = formRepository;
    }

    [HttpPost("{formId:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> SubmitAsync([FromBody] IList<FormFieldValue> fields, long formId,
      CancellationToken ct)
    {
      Form? form = await _formRepository.GetByIdAsync(formId, ct);
      if (form == null)
      {
        return NotFound();
      }

      await AppAuthorizationService.AdminOrMemberAsync(form.DashboardId)
        .OrThrowForbid();

      var submitResult = await _formValueService.SubmitAsync(form, fields, CurrentUserId,ct);
      if (submitResult.IsFailure)
      {
        return BadRequest();
      }

      return NoContent();
    }
  }
}