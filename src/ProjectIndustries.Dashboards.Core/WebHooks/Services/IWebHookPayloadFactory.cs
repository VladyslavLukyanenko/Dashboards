using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectIndustries.Dashboards.Core.WebHooks.Services
{
  public interface IWebHookPayloadFactory
  {
    ValueTask<WebHookPayloadEnvelop> CreateAsync<T>(WebHookBinding binding, T data, CancellationToken ct = default);
  }
}