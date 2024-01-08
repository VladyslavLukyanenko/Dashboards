using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using ProjectIndustries.Dashboards.App;
using ProjectIndustries.Dashboards.Core.Identity.Services;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.Products.Services;
using ProjectIndustries.Dashboards.Infra;
using TinyCsvParser;
using TinyCsvParser.Mapping;
using System.Linq;
using ProjectIndustries.Dashboards.Core.Identity;
using ProjectIndustries.Dashboards.Core.Primitives;
using TinyCsvParser.TypeConverter;

namespace ProjectIndustries.Dashboards.WebApi.Products
{
  public class DefaultDashboardSeeder : IDataSeeder
  {
    private const string DebugDashboard = "alantoo";
    private readonly IDashboardRepository _dashboardRepository;
    private readonly DbContext _context;
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserManager _userManager;


    public DefaultDashboardSeeder(IDashboardRepository dashboardRepository, DbContext context,
      IWebHostEnvironment hostingEnvironment, IUnitOfWork unitOfWork, IUserManager userManager)
    {
      _dashboardRepository = dashboardRepository;
      _context = context;
      _hostingEnvironment = hostingEnvironment;
      _unitOfWork = unitOfWork;
      _userManager = userManager;
    }

    public int Order => int.MaxValue;

    public async Task SeedAsync()
    {
      if (await _context.Set<Dashboard>()
        .AsQueryable()
        .AnyAsync(_ => _.HostingConfig.DomainName == DebugDashboard))
      {
        return;
      }


      var options = new CsvParserOptions(true, ',');
      var mapping = new SetupUserMapping();
      var parser = new CsvParser<SetupDashboard>(options, mapping);

      var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Setup", "Dashboards.csv");
      var results = parser.ReadFromFile(filePath, Encoding.UTF8)
        .ToList();

      if (!results.All(_ => _.IsValid))
      {
        throw new AppException("Can't read setup dashboards data");
      }

      var dashboards = results
        .Select(_ => _.Result)
        .ToLookup(_ => _.Email);

      foreach (var group in dashboards)
      {
        var setupUser = group.First();
        User user = User.CreateWithDiscordId(setupUser.Email, setupUser.Username, setupUser.DiscordUserId,
          setupUser.Avatar,
          setupUser.Discriminator);
        await _userManager.CreateAsync(user);
        var userDashboards = dashboards[setupUser.Email];
        foreach (var setupDashboard in userDashboards)
        {
          var dashboard = new Dashboard(setupDashboard.Dashboard, user.Id, setupDashboard.DiscordGuildId,
            setupDashboard.DiscordBotAccessToken, setupDashboard.DiscordAuthorizeUrl)
          {
            HostingConfig =
            {
              Mode = setupDashboard.DomainMode,
              DomainName = setupDashboard.Domain
            },
            DiscordConfig =
            {
              OAuthConfig =
              {
                Scope = setupDashboard.DiscordScopes,
                ClientId = setupDashboard.DiscordClientId,
                ClientSecret = setupDashboard.DiscordClientSecret,
                RedirectUrl = setupDashboard.DiscordRedirectUrl
              }
            }
          };

          await _dashboardRepository.CreateAsync(dashboard);
        }
      }

      await _unitOfWork.SaveEntitiesAsync();
    }

    private class SetupDashboard
    {
      public string Username { get; set; } = null!;
      public string Discriminator { get; set; } = null!;
      public string Email { get; set; } = null!;
      public ulong DiscordUserId { get; set; }
      public string DiscordAuthorizeUrl { get; set; } = null!;
      public string Avatar { get; set; } = null!;
      public string Dashboard { get; set; } = null!;
      public string DiscordBotAccessToken { get; set; } = null!;
      public string Domain { get; set; } = null!;
      public DashboardHostingMode DomainMode { get; set; }
      public ulong DiscordGuildId { get; set; }
      public string DiscordClientId { get; set; } = null!;
      public string DiscordClientSecret { get; set; } = null!;
      public string DiscordRedirectUrl { get; set; } = null!;
      public string DiscordScopes { get; set; } = null!;
    }

    private class SetupUserMapping : CsvMapping<SetupDashboard>
    {
      public SetupUserMapping()
      {
        MapProperty(0, _ => _.Username);
        MapProperty(1, _ => _.Discriminator);
        MapProperty(2, _ => _.Email);
        MapProperty(3, _ => _.DiscordUserId);
        MapProperty(4, _ => _.DiscordAuthorizeUrl);
        MapProperty(5, _ => _.Avatar);
        MapProperty(6, _ => _.Dashboard);
        MapProperty(7, _ => _.DiscordBotAccessToken);
        MapProperty(8, _ => _.Domain);
        MapProperty(9, _ => _.DomainMode, new EnumConverter<DashboardHostingMode>(true));
        MapProperty(10, _ => _.DiscordGuildId);
        MapProperty(11, _ => _.DiscordClientId);
        MapProperty(12, _ => _.DiscordClientSecret);
        MapProperty(13, _ => _.DiscordRedirectUrl);
        MapProperty(14, _ => _.DiscordScopes);
      }
    }
  }
}