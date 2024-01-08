using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ProjectIndustries.Dashboards.App.Products.Model;
using ProjectIndustries.Dashboards.Core.Collections;

namespace ProjectIndustries.Dashboards.App.Products.Services
{
  public interface IReleaseProvider
  {
    ValueTask<ReleaseData?> GetByIdAsync(long id, CancellationToken ct = default);
    ValueTask<IList<ActiveReleaseInfoData>> GetActiveListAsync(Guid dashboardId, CancellationToken ct = default);

    ValueTask<IPagedList<ReleaseRowData>> GetPageAsync(Guid dashboardId, ReleasesPageRequest pageRequest,
      CancellationToken ct = default);

    ValueTask<Maybe<ReleaseStockData?>> GetStockByPasswordAsync(string password, CancellationToken ct = default);
  }
}