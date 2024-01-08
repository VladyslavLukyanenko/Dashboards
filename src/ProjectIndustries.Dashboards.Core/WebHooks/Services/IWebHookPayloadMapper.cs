using System.Threading;
using System.Threading.Tasks;

namespace ProjectIndustries.Dashboards.Core.WebHooks.Services
{
  public interface IWebHookPayloadMapper
  {
    bool CanMap(object @event);

    ValueTask<WebHookPayloadEnvelop?> MapAsync(object @event, CancellationToken ct = default);
  }
}