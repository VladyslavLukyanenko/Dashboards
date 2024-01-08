using ProjectIndustries.Dashboards.App.Products.Model;

namespace ProjectIndustries.Dashboards.App.WebHooks
{
  public abstract class WebHookDataBase
  {
    public DashboardRef Dashboard { get; set; } = null!;
    public string Type => WebHookDataInspector.GetType(this);
  }
}