using System;
using ProjectIndustries.Dashboards.App.Identity.Model;
using ProjectIndustries.Dashboards.App.Model;

namespace ProjectIndustries.Dashboards.App.Commands
{
  public class CreateWithConfirmedEmailCommand : UserData
  {
    public string[] RoleNames { get; set; } = Array.Empty<string>();
    public string Password { get; set; } = null!;
  }
}