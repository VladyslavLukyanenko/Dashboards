using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ProjectIndustries.Dashboards.Core.Primitives;
using ProjectIndustries.Dashboards.Core.Services;
using ProjectIndustries.Dashboards.Core.WebHooks;
using ProjectIndustries.Dashboards.Core.WebHooks.Services;

namespace ProjectIndustries.Dashboards.Infra.WebHooks.Services
{
  public class HttpClientWebHookSender : IWebHookSender
  {
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IPublishedWebHookRepository _publishedWebHookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public HttpClientWebHookSender(IHttpClientFactory httpClientFactory, IJsonSerializer jsonSerializer,
      IPublishedWebHookRepository publishedWebHookRepository, IUnitOfWork unitOfWork)
    {
      _httpClientFactory = httpClientFactory;
      _jsonSerializer = jsonSerializer;
      _publishedWebHookRepository = publishedWebHookRepository;
      _unitOfWork = unitOfWork;
    }

    public async ValueTask<Result> SendAsync(WebHookPayloadEnvelop envelop, CancellationToken ct = default)
    {
      if (!envelop.Transport.HasFlag(WebHookDeliveryTransport.RemoteEndpoint))
      {
        return Result.Failure("Invalid payload provided. Its not configured for delivery to remote endpoint");
      }

      var client = _httpClientFactory.CreateClient(envelop.ListenerEndpoint!.Host);
      PublishedWebHook publishedWebHook;
      Result result;
      try
      {
        var serialized = await _jsonSerializer.SerializeAsync(envelop.Payload, ct);
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, envelop.ListenerEndpoint)
        {
          Content = new StringContent(serialized, Encoding.UTF8,
            "application/json")
        };

        var response = await client.SendAsync(requestMessage, ct);
        response.EnsureSuccessStatusCode();

        result = Result.Success();
        publishedWebHook = PublishedWebHook.Succeeded(envelop);
      }
      catch (Exception exc)
      {
        result = Result.Failure(exc.ToString());
        publishedWebHook = PublishedWebHook.Failure(envelop, exc);
      }

      await _publishedWebHookRepository.CreateAsync(publishedWebHook, ct);
      await _unitOfWork.SaveEntitiesAsync(ct);

      return result;
    }
  }
}