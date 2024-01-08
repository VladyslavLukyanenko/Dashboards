using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectIndustries.Dashboards.Core.Primitives;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.Products.Config;
using ProjectIndustries.Dashboards.Core.Products.Services;
using ProjectIndustries.Dashboards.Infra.Repositories;

namespace ProjectIndustries.Dashboards.Infra.Products.Services
{
  public class EfDashboardRepository : EfSoftRemovableCrudRepository<Dashboard, Guid>, IDashboardRepository
  {
    private readonly IQueryable<JoinedDashboard> _joinedDashboards;
    private readonly DashboardsConfig _config;

    public EfDashboardRepository(DbContext context, IUnitOfWork unitOfWork, DashboardsConfig config)
      : base(context, unitOfWork)
    {
      _config = config;
      _joinedDashboards = Context.Set<JoinedDashboard>();
    }

    public async ValueTask<Dashboard?> GetByRawLocationAsync(string rawLocation, CancellationToken ct = default)
    {
      var modes = HostingConfig.ResolvePossibleModes(rawLocation, _config);
      if (modes.IsFailure)
      {
        return null;
      }

      var normalizedModes = modes.Value.Select(m => $"{(int) m.Key}__{m.Value.ToLower()}");
      return await DataSource.FirstOrDefaultAsync(
        d => normalizedModes.Contains(d.HostingConfig.Mode + "__" + d.HostingConfig.DomainName.ToLower()), ct);
    }

    public async ValueTask<bool> AlreadyJoinedAsync(Guid dashboardId, long userId, CancellationToken ct = default)
    {
      return await _joinedDashboards.AnyAsync(_ => _.DashboardId == dashboardId && _.UserId == userId, ct);
    }

    public async ValueTask<JoinedDashboard> CreateJoinAsync(JoinedDashboard joinedDashboard,
      CancellationToken ct = default)
    {
      var e = await Context.AddAsync(joinedDashboard, ct);
      return e.Entity;
    }

    public async ValueTask<IList<AccessibleDashboard>> GetAccessibleDashboardsAsync(long userId,
      CancellationToken ct = default)
    {
      var ownDashboards = DataSource.Where(_ => _.OwnerId == userId)
        .Select(d => new AccessibleDashboard
        {
          Id = d.Id,
          IsProperty = true
        });

      var joinedDashboards = _joinedDashboards.Where(_ => _.UserId == userId)
        .Select(_ => new AccessibleDashboard
        {
          Id = _.DashboardId,
          IsProperty = false
        });

      return await ownDashboards.Union(joinedDashboards)
        .ToListAsync(ct);
    }

    public async ValueTask<Dashboard?> GetByOwnerIdAsync(long userId, CancellationToken ct = default)
    {
      return await DataSource.FirstOrDefaultAsync(_ => _.OwnerId == userId, ct);
    }
  }
}