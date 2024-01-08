using ProjectIndustries.Dashboards.App.Identity.Model;

namespace ProjectIndustries.Dashboards.App.Products.Model
{
  public class LicenseKeyShortData
  {
    public long Id { get; set; }
    public UserRef User { get; set; } = null!;
    public string Value { get; set; } = null!;
  }
}