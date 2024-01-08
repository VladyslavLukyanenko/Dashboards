using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectIndustries.Dashboards.Core.Primitives;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.Products.Services;
using ProjectIndustries.Dashboards.Infra.Repositories;

namespace ProjectIndustries.Dashboards.Infra.Products.Services
{
  public class EfPlanRepository : EfSoftRemovableCrudRepository<Plan, long>, IPlanRepository
  {
    public EfPlanRepository(DbContext context, IUnitOfWork unitOfWork)
      : base(context, unitOfWork)
    {
    }

    public async ValueTask<(ulong GuidId, IEnumerable<ulong> RoleIds)> GetDiscordRolesInfoAsync(long planId,
      CancellationToken ct = default)
    {
      var roleIds = from plan in DataSource
        join pr in Context.Set<Product>() on plan.ProductId equals pr.Id
        where plan.Id == planId
        select new
        {
          GuidId = pr.DiscordGuildId,
          ProductRoleId = pr.DiscordRoleId,
          PlanRoleId = plan.DiscordRoleId
        };

      var r = await roleIds.FirstOrDefaultAsync(ct);
      return (r.GuidId, new[] {r.ProductRoleId, r.PlanRoleId});
    }

    public async ValueTask<Plan?> GetByPasswordAsync(Guid dashboardId, string password, CancellationToken ct = default)
    {
      var releases = Context.Set<Release>().WhereNotRemoved();
      var query = from plan in DataSource
        join release in releases on plan.Id equals release.PlanId
        where plan.DashboardId == dashboardId && release.Password == password
        select plan;

      return await query.FirstOrDefaultAsync(ct);
    }
  }
}