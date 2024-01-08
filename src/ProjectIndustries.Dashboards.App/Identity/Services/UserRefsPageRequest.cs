using System.Collections.Generic;
using ProjectIndustries.Dashboards.App.Model;

namespace ProjectIndustries.Dashboards.App.Identity.Services
{
  public class UserRefsPageRequest : FilteredPageRequest
  {
    public long? FacilityId { get; set; }
    public long? SelectedId { get; set; }
    public List<long> ExcludedIds { get; set; } = new List<long>();
  }
}