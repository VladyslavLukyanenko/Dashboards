using System.Threading.Tasks;
using IdentityServer4.Validation;
using ProjectIndustries.Dashboards.App.Model.Discord;
using ProjectIndustries.Dashboards.App.Products.Services;
using ProjectIndustries.Dashboards.App.Security.Services;
using ProjectIndustries.Dashboards.App.Services.Discord;
using ProjectIndustries.Dashboards.Core.Identity.Services;
using ProjectIndustries.Dashboards.Core.Primitives;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.Products.Services;

namespace ProjectIndustries.Dashboards.WebApi.Services
{
  public class DiscordRefreshTokenGrantValidator : DiscordAuthenticationGrantValidatorBase
  {
    public const string GrantTypeName = "discord-token.refresh";

    public DiscordRefreshTokenGrantValidator(IDiscordClient discordClient, IUserManager userManager,
      IUserRepository userRepository, IUnitOfWork unitOfWork, IUserProfileService userProfileService,
      IDashboardManager dashboardManager, IDashboardRepository dashboardRepository,
      IMemberRoleProvider memberRoleProvider)
      : base(discordClient, userManager, userRepository, unitOfWork, userProfileService, dashboardManager,
        dashboardRepository, memberRoleProvider)
    {
    }

    public override string GrantType => GrantTypeName;

    protected override string? ExtractAuthPayload(ExtensionGrantValidationContext context)
    {
      return context.Request.Raw["refresh_token"];
    }

    protected override ValueTask<DiscordSecurityToken?> AuthenticateAsync(DiscordOAuthConfig config, string payload)
    {
      return DiscordClient.ReauthenticateAsync(config, payload);
    }
  }
}