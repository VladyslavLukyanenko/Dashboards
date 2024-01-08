using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.Identity;

namespace ProjectIndustries.Dashboards.Core.Products.Services
{
  public interface IDiscordService
  {
    ValueTask JoinToGuildAsync(Dashboard dashboard, string discordToken, User user, Plan plan, CancellationToken ct = default);
    ValueTask RemoveRolesByKeyAsync(LicenseKey key, Plan plan, CancellationToken ct = default);
  }
}