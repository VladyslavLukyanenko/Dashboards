using System;
using NodaTime;
using ProjectIndustries.Dashboards.Core.Products;

namespace ProjectIndustries.Dashboards.App.Products.Model
{
  public class DashboardData
  {
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;

    public StripeIntegrationConfig StripeConfig { get; set; } = null!;
    public Instant? ExpiresAt { get; set; }
    public DiscordConfig DiscordConfig { get; set; } = null!;
    public string? LogoSrc { get; set; }
    public string? CustomBackgroundSrc { get; set; }
    public string TimeZoneId { get; set; } = null!;
    public HostingConfig HostingConfig { get; set; } = null!;

    public bool ChargeBackersExportEnabled { get; set; }
  }
}