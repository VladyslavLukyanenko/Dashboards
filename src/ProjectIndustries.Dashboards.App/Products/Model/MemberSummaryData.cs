using System;
using System.Collections.Generic;
using NodaTime;
using ProjectIndustries.Dashboards.App.Model.Discord;
using ProjectIndustries.Dashboards.App.Security.Model;
using ProjectIndustries.Dashboards.Core.Identity;

namespace ProjectIndustries.Dashboards.App.Products.Model
{
  public class MemberSummaryData
  {
    public long UserId { get; set; }
    public Guid DashboardId { get; set; }
    public string Name { get; set; } = null!;
    public string Discriminator { get; set; } = null!;
    public ulong DiscordId { get; set; }
    public string? Avatar { get; set; }

    public Instant JoinedAt { get; set; }

    public IEnumerable<DiscordRoleInfo> DiscordRoles { get; set; } = new List<DiscordRoleInfo>();
    public IList<BoundMemberRoleData> Roles { get; set; } = new List<BoundMemberRoleData>();
  }
}