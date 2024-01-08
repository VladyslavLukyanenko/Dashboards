using ProjectIndustries.Dashboards.Core.Products;

namespace ProjectIndustries.Dashboards.Core.Forms
{
  public abstract class FormComponent : DashboardBoundEntity
  {
    public RichFormFieldTitle Name { get; set; } = new();
    public long FormId { get; private set; }
    public uint Order { get; set; }
    public string? Description { get; set; }
  }
}