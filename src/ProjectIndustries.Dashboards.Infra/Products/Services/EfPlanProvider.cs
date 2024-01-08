using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProjectIndustries.Dashboards.App.Products.Model;
using ProjectIndustries.Dashboards.App.Products.Services;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Infra.Services;

namespace ProjectIndustries.Dashboards.Infra.Products.Services
{
  public class EfPlanProvider : DataProvider, IPlanProvider
  {
    private readonly IQueryable<Plan> _plans;
    private readonly IMapper _mapper;

    public EfPlanProvider(DbContext context, IMapper mapper)
      : base(context)
    {
      _mapper = mapper;
      _plans = GetDataSource<Plan>();
    }

    public async ValueTask<IList<PlanData>> GetAllAsync(Guid dashboardId, CancellationToken ct = default)
    {
      return await _plans.Where(_ => _.DashboardId == dashboardId)
        .OrderByDescending(_ => _.CreatedAt)
        .ThenBy(_ => _.Description)
        .ProjectTo<PlanData>(_mapper.ConfigurationProvider)
        .ToListAsync(ct);
    }

    public async ValueTask<PlanData?> GetByIdAsync(long planId, CancellationToken ct = default)
    {
      var release = await _plans.FirstOrDefaultAsync(_ => _.Id == planId, ct);
      return _mapper.Map<PlanData>(release);
    }
  }
}