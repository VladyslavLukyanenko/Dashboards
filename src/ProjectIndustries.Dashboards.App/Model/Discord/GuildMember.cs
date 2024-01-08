using System.Collections.Generic;

namespace ProjectIndustries.Dashboards.App.Model.Discord
{
  public class GuildMember
  {
    public List<ulong> Roles { get; set; } = new();
  }
}