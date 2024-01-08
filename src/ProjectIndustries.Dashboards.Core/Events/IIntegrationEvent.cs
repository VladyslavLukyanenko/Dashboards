using NodaTime;

namespace ProjectIndustries.Dashboards.Core.Events
{
  public interface IIntegrationEvent
  {
    Instant Timestamp { get; }
  }
}