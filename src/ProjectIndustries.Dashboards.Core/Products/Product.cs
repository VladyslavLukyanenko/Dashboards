using System;
using System.Collections.Generic;
using System.Linq;
using ProjectIndustries.Dashboards.Core.Primitives;

namespace ProjectIndustries.Dashboards.Core.Products
{
  public class Product : DashboardBoundEntity
  {
    private Product()
    {
    }

    public Product(Guid dashboardId, string name, string description, string downloadUrl, Slug slug, Version version,
      ulong discordRoleId, ulong discordGuildId, string checkoutsTrackingWebhookUrl, IEnumerable<ProductFeature> features)
      : base(dashboardId)
    {
      Name = name;
      Description = description;
      DownloadUrl = downloadUrl;
      Slug = slug;
      Version = version;
      DiscordRoleId = discordRoleId;
      DiscordGuildId = discordGuildId;
      CheckoutsTrackingWebhookUrl = checkoutsTrackingWebhookUrl;
      Features = features.ToList();
    }

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IList<string> Images { get; set; } = new List<string>();
    public string DownloadUrl { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string LogoUrl { get; set; } = null!;
    public string IconUrl { get; set; } = null!;
    public Slug Slug { get; set; } = null!;
    public Version Version { get; set; } = null!;
    public ulong DiscordRoleId { get; set; }
    public ulong DiscordGuildId { get; set; }
    public string CheckoutsTrackingWebhookUrl { get; set; } = null!;
    public IList<ProductFeature> Features { get; set; } = new List<ProductFeature>();
  }
}