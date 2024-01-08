using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.App.Identity.Model;
using ProjectIndustries.Dashboards.Core.Collections;

namespace ProjectIndustries.Dashboards.App.Identity.Services
{
  public interface IUserRefProvider
  {
    ValueTask<UserRef> GetRefAsync(long userId, CancellationToken ct = default);
    ValueTask<IDictionary<long, UserRef>> GetRefsAsync(IEnumerable<long> userIds, CancellationToken ct = default);
    ValueTask<IPagedList<UserRef>> GetRefsPageAsync(UserRefsPageRequest pageRequest, CancellationToken ct);
  }
}