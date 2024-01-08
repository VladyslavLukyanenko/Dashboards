using System;
using CSharpFunctionalExtensions;

namespace ProjectIndustries.Dashboards.Core.WebHooks
{
  public class WebHooksConfig : Entity
  {
    public Guid DashboardId { get; set; }
    public bool IsEnabled { get; set; }
    public string ClientSecret { get; set; } = null!;
  }
}