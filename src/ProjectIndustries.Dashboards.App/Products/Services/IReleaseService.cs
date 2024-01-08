using System;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.Products;

namespace ProjectIndustries.Dashboards.App.Products.Services
{
  public interface IReleaseService
  {
    ValueTask<long> CreateAsync(Guid dashboardId, SaveReleaseCommand cmd, CancellationToken ct = default);
    ValueTask UpdateAsync(Release release, SaveReleaseCommand cmd, CancellationToken ct = default);
  }
}