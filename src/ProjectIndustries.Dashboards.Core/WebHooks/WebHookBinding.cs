using System;
using ProjectIndustries.Dashboards.Core.Primitives;

namespace ProjectIndustries.Dashboards.Core.WebHooks
{
  public class WebHookBinding : Entity
  {
    public string EventType { get; set; } = null!;
    public Uri? ListenerEndpoint { get; set; }
    public Guid DashboardId { get; set; }
    public WebHookDeliveryTransport Transport { get; set; }
  }
}