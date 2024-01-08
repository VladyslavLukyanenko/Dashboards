using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.Primitives;

namespace ProjectIndustries.Dashboards.Core.Forms.Services
{
  public interface IFormComponentRepository : ICrudRepository<FormComponent>
  {
    ValueTask<IList<FormField>> GetFieldsAsync(long formId, CancellationToken ct = default);
  }
}