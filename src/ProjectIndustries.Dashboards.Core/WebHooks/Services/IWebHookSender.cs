using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace ProjectIndustries.Dashboards.Core.WebHooks.Services
{
  public interface IWebHookSender
  {
    ValueTask<Result> SendAsync(WebHookPayloadEnvelop envelop, CancellationToken ct = default);
  }
}