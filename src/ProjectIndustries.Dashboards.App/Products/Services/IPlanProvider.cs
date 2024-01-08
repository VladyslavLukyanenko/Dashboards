using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.App.Products.Model;

namespace ProjectIndustries.Dashboards.App.Products.Services
{
  public interface IPlanProvider
  {
    ValueTask<IList<PlanData>> GetAllAsync(Guid dashboardId, CancellationToken ct = default);
    ValueTask<PlanData?> GetByIdAsync(long planId, CancellationToken ct = default);
  }
}