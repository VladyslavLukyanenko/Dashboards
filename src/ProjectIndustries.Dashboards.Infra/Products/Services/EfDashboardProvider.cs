using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProjectIndustries.Dashboards.App.Products.Model;
using ProjectIndustries.Dashboards.App.Products.Services;
using ProjectIndustries.Dashboards.App.Services;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Infra.Services;

namespace ProjectIndustries.Dashboards.Infra.Products.Services
{
  public class EfDashboardProvider : DataProvider, IDashboardProvider
  {
    private readonly IQueryable<Dashboard> _aliveDashboards;
    private readonly IMapper _mapper;
    private readonly IPathsService _pathsService;

    public EfDashboardProvider(DbContext context, IMapper mapper, IPathsService pathsService)
      : base(context)
    {
      _mapper = mapper;
      _pathsService = pathsService;
      _aliveDashboards = GetAliveDataSource<Dashboard>();
    }

    public async ValueTask<DashboardData?> GetByOwnerIdAsync(long ownerId, CancellationToken ct = default)
    {
      var data = await _aliveDashboards.Where(_ => _.OwnerId == ownerId)
        .ProjectTo<DashboardData>(_mapper.ConfigurationProvider)
        .FirstOrDefaultAsync(ct);

      data.LogoSrc = _pathsService.ToAbsoluteUrl(data.LogoSrc);
      data.CustomBackgroundSrc = _pathsService.ToAbsoluteUrl(data.CustomBackgroundSrc);
      return data;
    }

    public async ValueTask<DashboardLoginData?> GetLoginDataAsync(
      IEnumerable<KeyValuePair<DashboardHostingMode, string>> modes, CancellationToken ct = default)
    {
      var normalizedModes = modes.Select(m => $"{(int) m.Key}__{m.Value.ToLower()}");
      return await _aliveDashboards.Where(
          d => normalizedModes.Contains(d.HostingConfig.Mode + "__" + d.HostingConfig.DomainName.ToLower()))
        .Select(d => new DashboardLoginData
        {
          DashboardId = d.Id,
          DiscordAuthorizeUrl = d.DiscordConfig.OAuthConfig.AuthorizeUrl
        })
        .FirstOrDefaultAsync(ct);
    }
  }
}