using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectIndustries.Dashboards.Core.Embeds.Services
{
  public interface IEmbedRenderer
  {
    ValueTask<string> RenderAsync(DiscordEmbedWebHookBinding binding, IDictionary<string, object> context,
      CancellationToken ct = default);
  }
}