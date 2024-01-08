using ProjectIndustries.Dashboards.Core.Products;

namespace ProjectIndustries.Dashboards.App.Products.Model
{
  public class ReleaseRowData
  {
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public int InitialStock { get; set; }
    public int Stock { get; set; }
    public ReleaseType Type { get; set; }
    public string PlanDesc { get; set; } = null!;
    public bool IsActive { get; set; }
  }
}