using System;
using System.Threading;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace ProjectIndustries.Dashboards.Infra.Services
{
  public interface IDiscordClientProvider
  {
    ValueTask<DiscordSocketClient> GetInitializedClientAsync(Guid dashboardId, CancellationToken ct = default);
  }
}