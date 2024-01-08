using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectIndustries.Dashboards.Core.Primitives;
using ProjectIndustries.Dashboards.Core.WebHooks;
using ProjectIndustries.Dashboards.Core.WebHooks.Services;
using ProjectIndustries.Dashboards.Infra.Repositories;

namespace ProjectIndustries.Dashboards.Infra.WebHooks
{
  public class EfWebHookBindingRepository : EfCrudRepository<WebHookBinding>, IWebHookBindingRepository
  {
    public EfWebHookBindingRepository(DbContext context, IUnitOfWork unitOfWork)
      : base(context, unitOfWork)
    {
    }

    public ValueTask<WebHookBinding?> GetByTypeAsync(Guid dashboardId, string type, CancellationToken ct = default)
    {
      throw new NotImplementedException();
    }

    public ValueTask<WebHooksConfig> GetConfigOfDashboardAsync(Guid dashboardId, CancellationToken ct = default)
    {
      throw new NotImplementedException();
    }
  }
}