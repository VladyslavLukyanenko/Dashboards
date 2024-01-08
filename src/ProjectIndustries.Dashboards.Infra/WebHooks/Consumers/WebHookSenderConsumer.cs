using System.Threading.Tasks;
using MassTransit;
using ProjectIndustries.Dashboards.Core.WebHooks;
using ProjectIndustries.Dashboards.Core.WebHooks.Services;

namespace ProjectIndustries.Dashboards.Infra.WebHooks.Consumers
{
  public class WebHookSenderConsumer : IConsumer<WebHookPayloadEnvelop>
  {
    private readonly IWebHookSender _webHookSender;

    public WebHookSenderConsumer(IWebHookSender webHookSender)
    {
      _webHookSender = webHookSender;
    }

    public async Task Consume(ConsumeContext<WebHookPayloadEnvelop> context)
    {
      if (!context.Message.Transport.HasFlag(WebHookDeliveryTransport.RemoteEndpoint))
      {
        return; 
      }

      await _webHookSender.SendAsync(context.Message, context.CancellationToken);
    }
  }
}