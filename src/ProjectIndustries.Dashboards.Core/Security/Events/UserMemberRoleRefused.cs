﻿using System;
using ProjectIndustries.Dashboards.Core.Events;

namespace ProjectIndustries.Dashboards.Core.Security.Events
{
  public class UserMemberRoleRefused : DomainEvent
  {
    public UserMemberRoleRefused(long userId, long memberRoleId, Guid dashboardId)
    {
      UserId = userId;
      MemberRoleId = memberRoleId;
      DashboardId = dashboardId;
    }

    public long UserId { get; }
    public long MemberRoleId { get; }
    public Guid DashboardId { get; }
  }
}