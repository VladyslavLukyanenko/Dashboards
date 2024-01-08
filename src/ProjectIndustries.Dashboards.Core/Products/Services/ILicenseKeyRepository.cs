using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.Primitives;

namespace ProjectIndustries.Dashboards.Core.Products.Services
{
  public interface ILicenseKeyRepository : ICrudRepository<LicenseKey>
  {
    ValueTask<LicenseKey?> GetByValueAsync(string value, CancellationToken ct = default);
    ValueTask<IList<LicenseKey>> GetAllByPlanIdAsync(long planId, CancellationToken ct = default);
    ValueTask<LicenseKey?> GetBySubscriptionIdAsync(string subscriptionId, CancellationToken ct = default);
    ValueTask<bool> ExistsWithValueAsync(string keyValue, CancellationToken ct = default);
    ValueTask<bool> ExistsForPlanAsync(long userId, long planId, IReadOnlyCollection<long> exceptLicenseKeyIds, CancellationToken ct = default);
  }
}