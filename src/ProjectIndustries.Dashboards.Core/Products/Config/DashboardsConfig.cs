using ProjectIndustries.Dashboards.Core.FileStorage.Config;

namespace ProjectIndustries.Dashboards.Core.Products.Config
{
  public class DashboardsConfig
  {
    public string[] BlacklistedDomains { get; set; } = null!;
    public string LocationPathSegmentRegex { get; set; } = null!;
    
    
    public FileUploadsConfig LogoUploadConfig { get; set; } = null!;
    public FileUploadsConfig BackgroundUploadConfig { get; set; } = null!;
  }
}