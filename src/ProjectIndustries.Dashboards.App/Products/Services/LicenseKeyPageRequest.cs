using ProjectIndustries.Dashboards.App.Model;

namespace ProjectIndustries.Dashboards.App.Products.Services
{
  public class LicenseKeyPageRequest : FilteredPageRequest
  {
    public bool? LifetimeOnly { get; set; }
    public long? PlanId { get; set; }
    public long? ReleaseId { get; set; }
    public LicensesSortBy? SortBy { get; set; }
  }
}