using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace ProjectIndustries.Dashboards.Core.Embeds.Services
{
  public interface IEmbedMessageSender
  {
    ValueTask<Result> SendAsync(string serializedPayload, DiscordEmbedWebHookBinding binding, CancellationToken ct = default);
  }
}