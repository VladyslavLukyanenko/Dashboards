using NodaTime;

namespace ProjectIndustries.Dashboards.App.Analytics.Model
{
  public abstract class DiscordInsightsDataBase
  {
    public OffsetDateTime IntervalStartTimestamp { get; set; }
  }
}