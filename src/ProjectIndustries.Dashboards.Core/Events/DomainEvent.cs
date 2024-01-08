using NodaTime;

namespace ProjectIndustries.Dashboards.Core.Events
{
  public abstract class DomainEvent : IDomainEvent
  {
    public Instant Timestamp { get; } = SystemClock.Instance.GetCurrentInstant();
  }
}