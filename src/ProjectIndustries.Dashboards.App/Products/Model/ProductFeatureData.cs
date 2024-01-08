#pragma warning disable 8618
using ProjectIndustries.Dashboards.Core.FileStorage.FileSystem;

namespace ProjectIndustries.Dashboards.App.Products.Model
{
  public class ProductFeatureData
  {
    public string Icon { get; set; }
    public IBinaryData? UploadedIcon { get; set; }
    public string Title { get; set; }
    public string Desc { get; set; }
  }
}