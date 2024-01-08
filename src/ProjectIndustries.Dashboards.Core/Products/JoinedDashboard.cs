using System;
using NodaTime;

namespace ProjectIndustries.Dashboards.Core.Products
{
  public class JoinedDashboard
  {
    private JoinedDashboard()
    {
    }

    public JoinedDashboard(Guid dashboardId, long userId)
    {
      DashboardId = dashboardId;
      UserId = userId;
    }

    public Guid DashboardId { get; private set; }
    public long UserId { get; private set; }
    public Instant JoinedAt { get; private set; } = SystemClock.Instance.GetCurrentInstant();
  }
}