using System;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.App.Audit.Data;
using ProjectIndustries.Dashboards.Core.Collections;

namespace ProjectIndustries.Dashboards.App.Audit.Services
{
  public interface IChangeSetProvider
  {
    Task<IPagedList<ChangeSetData>> GetChangeSetPageAsync(ChangeSetPageRequest pageRequest,
      CancellationToken ct = default);

    Task<ChangesetEntryPayloadData?> GetPayloadAsync(Guid changeSetEntryId, CancellationToken ct = default);
  }
}