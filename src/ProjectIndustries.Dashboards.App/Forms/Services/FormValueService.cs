using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ProjectIndustries.Dashboards.Core.Forms;
using ProjectIndustries.Dashboards.Core.Forms.Services;

namespace ProjectIndustries.Dashboards.App.Forms.Services
{
  public class FormValueService : IFormValueService
  {
    private readonly IFormResponseRepository _formResponseRepository;
    private readonly IFormComponentRepository _formComponentRepository;

    public FormValueService(IFormResponseRepository formResponseRepository,
      IFormComponentRepository formComponentRepository)
    {
      _formResponseRepository = formResponseRepository;
      _formComponentRepository = formComponentRepository;
    }

    public async ValueTask<Result> SubmitAsync(Form form, IEnumerable<FormFieldValue> values, long userId,
      CancellationToken ct = default)
    {
      var existingResponses = await _formResponseRepository.GetByFormIdAsync(form.Id, userId, ct);
      if (form.Settings.LimitToSingleResponse && existingResponses.Count > 0)
      {
        return Result.Failure("Already responded");
      }

      var fields = await _formComponentRepository.GetFieldsAsync(form.Id, ct);
      Result<FormResponse> result = FormResponse.ValidateAndCreate(form.Id, userId, values, fields);
      if (result.IsFailure)
      {
        return result;
      }

      await _formResponseRepository.CreateAsync(result.Value, ct);

      return Result.Success();
    }
  }
}