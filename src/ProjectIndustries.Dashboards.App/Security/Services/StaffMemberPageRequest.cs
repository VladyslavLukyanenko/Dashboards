using ProjectIndustries.Dashboards.App.Model;

namespace ProjectIndustries.Dashboards.App.Security.Services
{
  public class StaffMemberPageRequest : FilteredPageRequest
  {
    /// <summary>
    /// Should we include member for which already assigned a role
    /// </summary>
    public bool IncludeStaff { get; set; }
  }
}