using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.Primitives;

namespace ProjectIndustries.Dashboards.Core.Security.Services
{
  public interface IUserMemberRoleBindingRepository : ICrudRepository<UserMemberRoleBinding>
  {
    ValueTask<UserMemberRoleBinding?> GetUserRoleBindingAsync(long userId, long memberRoleId,
      CancellationToken ct = default);

    ValueTask<IList<UserMemberRoleBinding>> GetBindingsAsync(Guid dashboardId, long memberRoleId,
      CancellationToken ct = default);
  }
}