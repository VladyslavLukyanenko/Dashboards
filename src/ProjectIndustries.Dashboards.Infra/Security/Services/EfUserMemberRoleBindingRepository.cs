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
  public class EfUserMemberRoleBindingRepository : EfCrudRepository<UserMemberRoleBinding>,
    IUserMemberRoleBindingRepository
  {
    public EfUserMemberRoleBindingRepository(DbContext context, IUnitOfWork unitOfWork)
      : base(context, unitOfWork)
    {
    }

    public async ValueTask<UserMemberRoleBinding?> GetUserRoleBindingAsync(long userId, long memberRoleId,
      CancellationToken ct = default)
    {
      return await DataSource.FirstOrDefaultAsync(_ => _.UserId == userId && _.MemberRoleId == memberRoleId, ct);
    }

    public async ValueTask<IList<UserMemberRoleBinding>> GetBindingsAsync(Guid dashboardId, long memberRoleId,
      CancellationToken ct = default)
    {
      return await DataSource.Where(_ => _.DashboardId == dashboardId && _.MemberRoleId == memberRoleId)
        .ToListAsync(ct);
    }
  }
}