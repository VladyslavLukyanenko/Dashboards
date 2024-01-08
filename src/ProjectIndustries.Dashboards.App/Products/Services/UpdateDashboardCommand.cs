using ProjectIndustries.Dashboards.App.Products.Model;
using ProjectIndustries.Dashboards.Core.FileStorage.FileSystem;

namespace ProjectIndustries.Dashboards.App.Products.Services
{
  public class UpdateDashboardCommand : DashboardData
  {
    public IBinaryData? UploadedLogoSrc { get; set; }
    public IBinaryData? UploadedCustomBackgroundSrc { get; set; }
  }
}