using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ProjectIndustries.Dashboards.Core.FileStorage.FileSystem;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.Products.Config;
using ProjectIndustries.Dashboards.Core.Products.Services;

namespace ProjectIndustries.Dashboards.App.Products.Services
{
  public class DashboardService : IDashboardService
  {
    private readonly IDashboardRepository _dashboardRepository;
    private readonly IMapper _mapper;
    private readonly DashboardsConfig _config;
    private readonly IFileUploadService _fileUploadService;

    public DashboardService(IDashboardRepository dashboardRepository, IMapper mapper, DashboardsConfig config,
      IFileUploadService fileUploadService)
    {
      _dashboardRepository = dashboardRepository;
      _mapper = mapper;
      _config = config;
      _fileUploadService = fileUploadService;
    }

    public async ValueTask UpdateAsync(Dashboard dashboard, UpdateDashboardCommand cmd, CancellationToken ct = default)
    {
      _mapper.Map(cmd, dashboard);
      _mapper.Map(cmd.DiscordConfig, dashboard.DiscordConfig);
      _mapper.Map(cmd.StripeConfig, dashboard.StripeConfig);
      _mapper.Map(cmd.HostingConfig, dashboard.HostingConfig);
      dashboard.LogoSrc = await _fileUploadService.UploadFileOrDefaultAsync(cmd.UploadedLogoSrc,
        _config.LogoUploadConfig, dashboard.LogoSrc, ct);

      dashboard.CustomBackgroundSrc = await _fileUploadService.UploadFileOrDefaultAsync(cmd.UploadedCustomBackgroundSrc,
        _config.BackgroundUploadConfig, dashboard.CustomBackgroundSrc, ct);

      _dashboardRepository.Update(dashboard);
    }
  }
}