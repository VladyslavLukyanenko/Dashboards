using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectIndustries.Dashboards.Core.Events;
using ProjectIndustries.Dashboards.Core.Primitives;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.Products.Events.LicenseKeys;
using ProjectIndustries.Dashboards.Core.Products.Services;
using ProjectIndustries.Dashboards.Infra.Repositories;

namespace ProjectIndustries.Dashboards.Infra.Products.Services
{
  public class EfLicenseKeyRepository : EfSoftRemovableCrudRepository<LicenseKey, long>, ILicenseKeyRepository
  {
    public EfLicenseKeyRepository(DbContext context, IUnitOfWork unitOfWork)
      : base(context, unitOfWork)
    {
    }

    public async ValueTask<LicenseKey?> GetByValueAsync(string value, CancellationToken ct = default)
    {
      return await DataSource.FirstOrDefaultAsync(_ => _.Value == value, ct);
    }

    public async ValueTask<IList<LicenseKey>> GetAllByPlanIdAsync(long planId, CancellationToken ct = default)
    {
      return await DataSource.Where(_ => _.PlanId == planId).ToArrayAsync(ct);
    }

    public async ValueTask<LicenseKey?> GetBySubscriptionIdAsync(string subscriptionId, CancellationToken ct = default)
    {
      return await DataSource.FirstOrDefaultAsync(_ => _.SubscriptionId == subscriptionId, ct);
    }

    public async ValueTask<bool> ExistsWithValueAsync(string keyValue, CancellationToken ct = default)
    {
      return await DataSource.AnyAsync(_ => _.Value == keyValue, ct);
    }

    public async ValueTask<bool> ExistsForPlanAsync(long userId, long planId,
      IReadOnlyCollection<long> exceptLicenseKeyIds, CancellationToken ct = default)
    {
      var query = DataSource;
      if (exceptLicenseKeyIds.Any())
      {
        query = query.Where(_ => !exceptLicenseKeyIds.Contains(_.Id));
      }

      return await query.AnyAsync(_ => _.UserId == userId && _.PlanId == planId, ct);
    }

    protected override DomainEvent CreateRemovedEvent(LicenseKey entity) => new LicenseKeyRemoved(entity);
  }
}