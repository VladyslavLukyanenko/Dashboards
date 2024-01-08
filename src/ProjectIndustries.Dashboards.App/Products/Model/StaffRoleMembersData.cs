using System.Collections.Generic;

namespace ProjectIndustries.Dashboards.App.Products.Model
{
  public class StaffRoleMembersData
  {
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public IList<StaffMemberData> Members { get; set; } = new List<StaffMemberData>();
  }
}