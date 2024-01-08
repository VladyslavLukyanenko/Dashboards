using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectIndustries.Dashboards.Core.Embeds;
using ProjectIndustries.Dashboards.Core.Embeds.Services;
using ProjectIndustries.Dashboards.Core.Primitives;
using ProjectIndustries.Dashboards.Infra.Repositories;

namespace ProjectIndustries.Dashboards.Infra.Embeds
{
  public class EfDiscordEmbedWebHookBindingRepository : EfCrudRepository<DiscordEmbedWebHookBinding>,
    IDiscordEmbedWebHookBindingRepository
  {
    public EfDiscordEmbedWebHookBindingRepository(DbContext context, IUnitOfWork unitOfWork)
      : base(context, unitOfWork)
    {
    }

    public ValueTask<DiscordEmbedWebHookBinding?> GetByEventTypeAsync(Guid dashboardId, string eventType,
      CancellationToken ct = default)
    {
      throw new NotImplementedException();
    }
  }
}