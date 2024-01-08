using System;
using ProjectIndustries.Dashboards.Core.Primitives;

namespace ProjectIndustries.Dashboards.Core.Products
{
  public interface IDashboardBoundEntity
  {
    Guid DashboardId { get; }
  }
}