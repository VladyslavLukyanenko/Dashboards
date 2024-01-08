using System;
using NodaTime;
#pragma warning disable 8618

namespace ProjectIndustries.Dashboards.App.Products.Model
{
  public class PlanData
  {
    public long Id { get; set; }
    public Guid DashboardId { get; set; }
    public long ProductId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string? SubscriptionPlan { get; set; }
    public Period? LicenseLife { get; set; }
    public string Description { get; set; }
    public Period? UnbindableDelay { get; set; }
    public bool IsTrial { get; set; }

    public ulong DiscordRoleId { get; set; }

    // public bool IsUnbindable { get; set; }
  }
}