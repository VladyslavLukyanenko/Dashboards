using System;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.App.Products.Collections;

namespace ProjectIndustries.Dashboards.App.Products.Services
{
  public interface IGlobalSearchProvider
  {
    ValueTask<IGlobalSearchPagedList> GetAllAsync(Guid dashboardId, GlobalSearchPageRequest pageRequest,
      CancellationToken ct = default);
  }
}