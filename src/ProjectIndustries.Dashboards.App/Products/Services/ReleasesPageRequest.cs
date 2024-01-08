using ProjectIndustries.Dashboards.App.Model;
using ProjectIndustries.Dashboards.Core.Products;

namespace ProjectIndustries.Dashboards.App.Products.Services
{
  public class ReleasesPageRequest : FilteredPageRequest
  {
    public long? PlanId { get; set; }
    public ReleaseType? Type { get; set; }
    public ReleasesSortBy? SortBy { get; set; }
  }
}