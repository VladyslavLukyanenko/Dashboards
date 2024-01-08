using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.Primitives;

namespace ProjectIndustries.Dashboards.Core.ChargeBackers.Services
{
  public interface IChargeBackerRepository : ICrudRepository<ChargeBacker>
  {
    ValueTask<IList<ChargeBacker>> GetNotExportedAsync(Guid dashboardId, CancellationToken ct = default);
  }
}