using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ProjectIndustries.Dashboards.Core.Forms;

namespace ProjectIndustries.Dashboards.App.Forms.Services
{
  public interface IFormValueService
  {
    ValueTask<Result> SubmitAsync(Form form, IEnumerable<FormFieldValue> values, long userId,
      CancellationToken ct = default);
  }
}