﻿using System;
using ProjectIndustries.Dashboards.Core.Events;

namespace ProjectIndustries.Dashboards.Core.Security.Events
{
  public class UserMemberRoleGranted : DomainEvent
  {
    public UserMemberRoleGranted(long userId, long memberRoleId, decimal? salary, Guid dashboardId)
    {
      UserId = userId;
      MemberRoleId = memberRoleId;
      Salary = salary;
      DashboardId = dashboardId;
    }

    public long UserId { get; }
    public long MemberRoleId { get; }
    public decimal? Salary { get; }
    public Guid DashboardId { get; }
  }
}