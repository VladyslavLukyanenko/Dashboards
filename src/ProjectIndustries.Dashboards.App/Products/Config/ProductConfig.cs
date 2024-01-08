#pragma warning disable 8618
using ProjectIndustries.Dashboards.App.Config;
using ProjectIndustries.Dashboards.Core.FileStorage.Config;

namespace ProjectIndustries.Dashboards.App.Products.Config
{
  public class ProductConfig
  {
    public FileUploadsConfig ImageUploadConfig { get; set; }
    public FileUploadsConfig LogoUploadConfig { get; set; }
    public FileUploadsConfig IconUploadConfig { get; set; }
    public FileUploadsConfig ImagesUploadConfig { get; set; }
    public FileUploadsConfig FeatureIconUploadConfig { get; set; }
  }
}