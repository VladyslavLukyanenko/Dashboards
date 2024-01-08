using System;

namespace ProjectIndustries.Dashboards.App.Products.Model
{
  public class DashboardLoginData
  {
    public Guid DashboardId { get; set; }
    public string DiscordAuthorizeUrl { get; set; } = null!;
  }
}