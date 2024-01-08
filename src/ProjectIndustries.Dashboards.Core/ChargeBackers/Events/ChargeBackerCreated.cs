using System;
using ProjectIndustries.Dashboards.Core.Events;

namespace ProjectIndustries.Dashboards.Core.ChargeBackers.Events
{
  public class ChargeBackerCreated : DomainEvent
  {
    public long ChargeBackerId { get; private set; }
    public Guid DashboardId { get; private set; }
  }
}