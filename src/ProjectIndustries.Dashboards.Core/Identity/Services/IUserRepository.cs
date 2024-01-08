using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.Primitives;

namespace ProjectIndustries.Dashboards.Core.Identity.Services
{
  public interface IUserRepository : ICrudRepository<User>
  {
    Task<IList<string>> GetRolesAsync(long userId, CancellationToken ct = default);
    Task<User> GetByEmailAsync(string subject, CancellationToken ct = default);
    Task<User> GetByDiscordIdAsync(ulong discordId, CancellationToken ct = default);
  }
}