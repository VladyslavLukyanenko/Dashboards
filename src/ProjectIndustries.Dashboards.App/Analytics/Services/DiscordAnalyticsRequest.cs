using NodaTime;
using ProjectIndustries.Dashboards.Core.Collections;

namespace ProjectIndustries.Dashboards.App.Analytics.Services
{
  public class DiscordAnalyticsRequest
  {
    public DiscordInsightsInterval Interval { get; set; }
    public Instant StartAt { get; set; }
    public Instant EndAt { get; set; }
  }
}