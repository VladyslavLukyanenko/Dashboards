using System.Collections.Generic;

namespace ProjectIndustries.Dashboards.App.Analytics.Model
{
  public class VisitorsStats
  {
    public ValueDiff<int> Count { get; set; } = null!;
    public IList<VisitorsStatsItem> Data { get; set; } = new List<VisitorsStatsItem>();
  }
}