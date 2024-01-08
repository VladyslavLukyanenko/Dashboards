using System;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.Primitives;

namespace ProjectIndustries.Dashboards.Core.Embeds.Services
{
  public interface IDiscordEmbedWebHookBindingRepository : ICrudRepository<DiscordEmbedWebHookBinding>
  {
    ValueTask<DiscordEmbedWebHookBinding?> GetByEventTypeAsync(Guid dashboardId, string eventType,
      CancellationToken ct = default);
  }
}