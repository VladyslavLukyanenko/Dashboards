using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectIndustries.Dashboards.Core.Primitives;
using ProjectIndustries.Dashboards.Core.Security;
using ProjectIndustries.Dashboards.Core.Security.Services;
using ProjectIndustries.Dashboards.Infra.Repositories;

namespace ProjectIndustries.Dashboards.Infra.Security.Services
{
  public class EfMemberRoleRepository : EfCrudRepository<MemberRole>, IMemberRoleRepository
  {
    public EfMemberRoleRepository(DbContext context, IUnitOfWork unitOfWork)
      : base(context, unitOfWork)
    {
    }

    public async ValueTask<IList<MemberRole>> GetDashboardRolesByIdsAsync(Guid dashboardId, IEnumerable<long> roleIds,
      CancellationToken ct = default)
    {
      return await DataSource.Where(_ => _.DashboardId == dashboardId && roleIds.Contains(_.Id))
        .ToListAsync(ct);
    }
  }
}