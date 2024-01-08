using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.Primitives;

namespace ProjectIndustries.Dashboards.Core.Forms.Services
{
  public interface IFormResponseRepository : ICrudRepository<FormResponse>
  {
    // ValueTask<bool> AlreadyRespondedAsync(long formId, long userId, CancellationToken ct = default);
    ValueTask<IList<FormResponse>> GetByFormIdAsync(long formId, long userId, CancellationToken ct = default);
  }
}