using System;
using NodaTime;

namespace ProjectIndustries.Dashboards.Core.Products
{
  public class Plan : DashboardBoundEntity
  {
    private static readonly Period DefaultLicenseLifetime = Period.FromDays(30);
    private static readonly Period DefaultUnbindableDelay = Period.FromDays(30);

    public Plan(Guid dashboardId, long productId)
      : base(dashboardId)
    {
      ProductId = productId;
    }

    public bool IsUnbindable() => UnbindableDelay != null;

    public Instant? CalculateKeyExpiry() => IsTrial || LicenseLife == null
      ? (Instant?) null
      : SystemClock.Instance.GetCurrentInstant() + LicenseLife.ToDuration();

    public Instant CalculatePossibleKeyExpiry() =>
      SystemClock.Instance.GetCurrentInstant() + (LicenseLife ?? DefaultLicenseLifetime).ToDuration();

    public Instant? CalculateUnbindableAfter() => UnbindableDelay == null
      ? (Instant?) null
      : SystemClock.Instance.GetCurrentInstant() + UnbindableDelay.ToDuration();

    public Instant CalculatePossibleUnbindableAfter() =>
      SystemClock.Instance.GetCurrentInstant() + (UnbindableDelay ?? DefaultUnbindableDelay).ToDuration();

    public bool IsLifetimeLimited() => LicenseLife != null;

    public long ProductId { get; private set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = null!;
    public string? SubscriptionPlan { get; set; }
    public Period? LicenseLife { get; set; }
    public Period TrialPeriod { get; set; } = Period.FromDays(30);
    public string Description { get; set; } = null!;
    public Period? UnbindableDelay { get; set; }
    public bool IsTrial { get; set; }

    public ulong DiscordRoleId { get; set; }
    public bool ProtectPurchasesWithCaptcha { get; set; }

    public LicenseKeyGeneratorConfig LicenseKeyConfig { get; set; } = LicenseKeyGeneratorConfig.RandomString();
  }
}