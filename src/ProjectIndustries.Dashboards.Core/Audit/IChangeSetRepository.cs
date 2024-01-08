using System;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.Primitives;

namespace ProjectIndustries.Dashboards.Core.Audit
{
  public interface IChangeSetRepository : IRepository<ChangeSet, Guid>
  {
    Task<ChangeSetEntry?> GetPreviousAsync(ChangeSetEntry current, CancellationToken ct = default);
    Task<ChangeSetEntry?> GetEntryByIdAsync(Guid changeSetEntryId, CancellationToken ct = default);
  }
}