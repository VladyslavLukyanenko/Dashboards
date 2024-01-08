using System;
using ProjectIndustries.Dashboards.Core.Events;

namespace ProjectIndustries.Dashboards.Core.Products.Events
{
  public interface IDashboardBoundEvent : IIntegrationEvent
  {
    Guid DashboardId { get; }
  }
}