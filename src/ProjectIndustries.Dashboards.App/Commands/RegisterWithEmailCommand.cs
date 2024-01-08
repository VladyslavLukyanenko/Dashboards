using ProjectIndustries.Dashboards.App.Identity.Model;
using ProjectIndustries.Dashboards.App.Model;

namespace ProjectIndustries.Dashboards.App.Commands
{
  public class RegisterWithEmailCommand : UserData
  {
    public string Password { get; set; } = null!;
  }
}