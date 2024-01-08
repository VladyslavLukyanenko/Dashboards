using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using ProjectIndustries.Dashboards.App.Products.Services;
using ProjectIndustries.Dashboards.Core.Identity;
using ProjectIndustries.Dashboards.Core.Identity.Services;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.Products.Services;
using IDiscordClient = ProjectIndustries.Dashboards.App.Services.Discord.IDiscordClient;

namespace ProjectIndustries.Dashboards.Infra.Services
{
  public class DiscordService : IDiscordService
  {
    private readonly IProductProvider _productProvider;
    private readonly IDiscordClient _discordClient;
    private readonly IDiscordClientProvider _discordClientProvider;
    private readonly IUserRepository _userRepository;

    public DiscordService(IProductProvider productProvider, IDiscordClient discordClient,
      IDiscordClientProvider discordClientProvider, IUserRepository userRepository)
    {
      _productProvider = productProvider;
      _discordClient = discordClient;
      _discordClientProvider = discordClientProvider;
      _userRepository = userRepository;
    }

    public async ValueTask JoinToGuildAsync(Dashboard dashboard, string discordToken, User user, Plan plan,
      CancellationToken ct = default)
    {
      var discordUser = await _discordClient.GetProfileAsync(discordToken, ct);
      await _discordClient.JoinGuildAsync(dashboard.DiscordConfig, discordToken, discordUser!, ct);
    }

    public async ValueTask RemoveRolesByKeyAsync(LicenseKey key, Plan plan, CancellationToken ct = default)
    {
      var product = await _productProvider.GetByIdAsync(key.ProductId, ct);
      var client = await _discordClientProvider.GetInitializedClientAsync(key.DashboardId, ct);
      var guild = client.GetGuild(product!.DiscordGuildId);

      IEnumerable<IRole> roles = new IRole[] {guild.GetRole(product.DiscordRoleId), guild.GetRole(plan.DiscordRoleId)};

      if (key.UserId.HasValue)
      {
        var user = await _userRepository.GetByIdAsync(key.UserId.Value, ct);
        var guildUser = guild.GetUser(user!.DiscordId);
        await guildUser.RemoveRolesAsync(roles);
      }
    }
  }
}