using System;
using NodaTime;
using ProjectIndustries.Dashboards.Core.Identity;
using ProjectIndustries.Dashboards.Core.Primitives;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.Security.Events;

namespace ProjectIndustries.Dashboards.Core.Security
{
  public class UserMemberRoleBinding : AuditableEntity, IDashboardBoundEntity
  {
    private UserMemberRoleBinding()
    {
    }

    public UserMemberRoleBinding(MemberRole role, User user)
    {
      MemberRoleId = role.Id;
      UserId = user.Id;
      DashboardId = role.DashboardId;

      AddDomainEvent(new UserMemberRoleGranted(user.Id, role.Id, role.Salary, role.DashboardId));
    }

    public long MemberRoleId { get; private set; }
    public long UserId { get; private set; }
    public Instant? LastPaidOutAt { get; private set; }
    public string? RemoteAccountId { get; set; }
    public Guid DashboardId { get; private set; }

    public Instant GetLastPaidOutAtOrDefault() => LastPaidOutAt.GetValueOrDefault(CreatedAt);
  }
}