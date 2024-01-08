using NodaTime;

namespace ProjectIndustries.Dashboards.Core.Primitives
{
  public interface ITimestampAuditable
  {
    Instant CreatedAt { get; }
    Instant UpdatedAt { get; }
  }
}