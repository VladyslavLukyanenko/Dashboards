using System;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.Primitives;

namespace ProjectIndustries.Dashboards.Core.WebHooks.Services
{
  public interface IWebHookBindingRepository : ICrudRepository<WebHookBinding>
  {
    ValueTask<WebHookBinding?> GetByTypeAsync(Guid dashboardId, string type, CancellationToken ct = default);
    ValueTask<WebHooksConfig> GetConfigOfDashboardAsync(Guid dashboardId, CancellationToken ct = default);
  }
}