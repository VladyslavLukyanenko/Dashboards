using NodaTime;
using ProjectIndustries.Dashboards.Core.Products;

namespace ProjectIndustries.Dashboards.Core.Forms
{
  public class Form : DashboardBoundEntity
  {
    public Instant? PublishedAt { get; set; } 
    public FormSettings Settings { get; set; } = new();
    public FormThemeSettings Theme { get; set; } = new();
  }
}