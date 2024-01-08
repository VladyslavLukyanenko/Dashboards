#pragma warning disable 8618
using System;
using System.Collections.Generic;
using ProjectIndustries.Dashboards.Core.FileStorage.FileSystem;

namespace ProjectIndustries.Dashboards.App.Products.Model
{
  public class ProductData
  {
    public long Id { get; set; }
    public Guid DashboardId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Images { get; set; } = new();
    public string DownloadUrl { get; set; }
    public string ImageUrl { get; set; }
    public string LogoUrl { get; set; }
    public string IconUrl { get; set; }
    public string Slug { get; set; }
    public string Version { get; set; }
    public ulong DiscordRoleId { get; set; }
    public ulong DiscordGuildId { get; set; }
    public string CheckoutsTrackingWebhookUrl { get; set; }
    public IList<ProductFeatureData> Features { get; set; } = new List<ProductFeatureData>();


    public IBinaryData? UploadedImage { get; set; }
    public IBinaryData? UploadedLogo { get; set; }
    public IBinaryData? UploadedIcon { get; set; }

    public IList<IBinaryData> UploadedImages { get; set; } = new List<IBinaryData>();
  }
}