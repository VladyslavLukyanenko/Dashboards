using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectIndustries.Dashboards.Core.ChargeBackers;
using ProjectIndustries.Dashboards.Core.ChargeBackers.Services;
using ProjectIndustries.Dashboards.Core.Primitives;
using ProjectIndustries.Dashboards.Infra.Repositories;

namespace ProjectIndustries.Dashboards.Infra.ChargeBackers
{
  public class EfChargeBackerRepository : EfCrudRepository<ChargeBacker>, IChargeBackerRepository
  {
    public EfChargeBackerRepository(DbContext context, IUnitOfWork unitOfWork)
      : base(context, unitOfWork)
    {
    }

    public ValueTask<IList<ChargeBacker>> GetNotExportedAsync(Guid dashboardId, CancellationToken ct = default)
    {
      throw new NotImplementedException();
    }
  }
}