using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.Primitives;

namespace ProjectIndustries.Dashboards.Core.Products.Services
{
  public interface IPlanRepository : ICrudRepository<Plan>
  {
    ValueTask<(ulong GuidId, IEnumerable<ulong> RoleIds)> GetDiscordRolesInfoAsync(long planId,
      CancellationToken ct = default);

    ValueTask<Plan?> GetByPasswordAsync(Guid dashboardId, string password, CancellationToken ct = default);
  }
}