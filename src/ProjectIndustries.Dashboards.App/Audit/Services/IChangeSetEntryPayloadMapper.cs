using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.Audit;

namespace ProjectIndustries.Dashboards.App.Audit.Services
{
  public interface IChangeSetEntryPayloadMapper
  {
    Task<IDictionary<string, string?>?> MapAsync(ChangeSetEntry entry, CancellationToken ct = default);
  }
}