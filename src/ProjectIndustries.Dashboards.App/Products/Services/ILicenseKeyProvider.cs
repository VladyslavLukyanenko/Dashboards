using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NodaTime;
using ProjectIndustries.Dashboards.App.Products.Model;
using ProjectIndustries.Dashboards.Core.Collections;

namespace ProjectIndustries.Dashboards.App.Products.Services
{
  public interface ILicenseKeyProvider
  {
    ValueTask<LicenseKeySummaryData> GetSummaryByIdAsync(long id, CancellationToken ct = default);
    ValueTask<int> GetUsedTodayCountAsync(Guid dashboardId, Offset offset, CancellationToken ct = default);

    ValueTask<IList<LicenseKeySnapshotData>>
      GetAllByProductIdAsync(long userId, long productId, CancellationToken ct = default);

    ValueTask<IPagedList<LicenseKeyShortData>> GetPageByReleaseIdAsync(long releaseId, PageRequest pageRequest,
      CancellationToken ct = default);

    ValueTask<IPagedList<LicenseKeySnapshotData>> GetPageAsync(Guid dashboardId, LicenseKeyPageRequest pageRequest,
      CancellationToken ct = default);
  }
}