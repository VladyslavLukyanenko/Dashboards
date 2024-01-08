using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectIndustries.Dashboards.Core.Products.Services;
using ProjectIndustries.Dashboards.WebApi.Services.Stripe;
using Stripe;
using ControllerBase = ProjectIndustries.Dashboards.WebApi.Foundation.Mvc.Controllers.ControllerBase;

namespace ProjectIndustries.Dashboards.WebApi.Controllers
{
  public class StripeWebHooksController : ControllerBase
  {
    private readonly IDashboardRepository _dashboardRepository;
    private readonly IEnumerable<IStripeWebHookHandler> _webHookHandlers;
    private readonly ILogger<StripeWebHooksController> _logger;

    public StripeWebHooksController(IDashboardRepository dashboardRepository,
      IEnumerable<IStripeWebHookHandler> webHookHandlers, ILogger<StripeWebHooksController> logger)
    {
      _dashboardRepository = dashboardRepository;
      _webHookHandlers = webHookHandlers;
      _logger = logger;
    }

    [HttpPost("{dashboardId:guid}/Webhooks")]
    public async ValueTask<IActionResult> HandleWebhookAsync([FromHeader(Name = "Stripe-Signature")]
      string signature, [FromBody] string rawPayload, Guid dashboardId, CancellationToken ct)
    {
      try
      {
        _logger.LogDebug("Received stripe webhook. Searching for dashboard with Id {DashboardId}", dashboardId);
        var dashboard = await _dashboardRepository.GetByIdAsync(dashboardId, ct);
        if (dashboard == null)
        {
          _logger.LogWarning("Dashboard {DashboardId} not found", dashboardId);
          return NotFound();
        }

        _logger.LogDebug("Constructing event");
        var @event = EventUtility.ConstructEvent(rawPayload, signature, dashboard.StripeConfig.WebHookEndpointSecret);
        _logger.LogDebug("Event {EventType} constructed", @event.Type);

        var handler = _webHookHandlers.FirstOrDefault(_ => _.CanHandle(@event.Type));
        if (handler == null)
        {
          _logger.LogWarning("Not handler for event {EventType}", @event.Type);
          return NoContent();
        }

        _logger.LogDebug("Handling event {EventType}", @event.Type);
        var result = await handler.HandleAsync(@event, dashboard, ct);
        if (result.IsFailure)
        {
          _logger.LogError("Can't handle webhook. {Reason}", result.Error);
          return BadRequest();
        }

        _logger.LogDebug("Event {EventType} handled successfully", @event.Type);
        return NoContent();
      }
      catch (StripeException exc)
      {
        _logger.LogError(exc, "Can't handle stripe webhook for dashboard {DashboardId}", dashboardId);
        return BadRequest();
      }
    }
  }
}