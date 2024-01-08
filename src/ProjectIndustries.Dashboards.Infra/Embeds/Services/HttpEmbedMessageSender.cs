using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ProjectIndustries.Dashboards.Core.Embeds;
using ProjectIndustries.Dashboards.Core.Embeds.Services;

namespace ProjectIndustries.Dashboards.Infra.Embeds.Services
{
  public class HttpEmbedMessageSender : IEmbedMessageSender
  {
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpEmbedMessageSender(IHttpClientFactory httpClientFactory)
    {
      _httpClientFactory = httpClientFactory;
    }

    public async ValueTask<Result> SendAsync(string serializedPayload, DiscordEmbedWebHookBinding binding,
      CancellationToken ct = default)
    {
      var client = _httpClientFactory.CreateClient("EmbedMessageSender");
      var webhookContent = new StringContent(serializedPayload, Encoding.UTF8, "application/json");
      var webhookResp = await client.PostAsync(binding.WebhookUrl, webhookContent, ct);

      return webhookResp.IsSuccessStatusCode
        ? Result.Success()
        : Result.Failure("Failed to submit webhook: " + await webhookResp.Content.ReadAsStringAsync(ct));
    }
  }
}