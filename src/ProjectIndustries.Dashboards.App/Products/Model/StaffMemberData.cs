namespace ProjectIndustries.Dashboards.App.Products.Model
{
  public class StaffMemberData
  {
    /// <summary>
    /// User id
    /// </summary>
    public long Id { get; set; }
    public string Discriminator { get; set; } = null!;
    public string? Avatar { get; set; }
    public string Name { get; set; } = null!;
  }
}