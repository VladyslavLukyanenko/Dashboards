using System;
using ProjectIndustries.Dashboards.Core.Primitives;

namespace ProjectIndustries.Dashboards.Core.Products
{
  public abstract class DashboardBoundEntity : SoftRemovableEntity, IDashboardBoundEntity
  {
    protected DashboardBoundEntity()
    {
    }

    protected DashboardBoundEntity(Guid dashboardId)
    {
      DashboardId = dashboardId;
    }

    public Guid DashboardId { get; private set; }
  }
}