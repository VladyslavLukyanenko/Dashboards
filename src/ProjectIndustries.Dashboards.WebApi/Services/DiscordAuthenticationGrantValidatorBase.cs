using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using ProjectIndustries.Dashboards.App.Model.Discord;
using ProjectIndustries.Dashboards.App.Products.Services;
using ProjectIndustries.Dashboards.App.Security.Services;
using ProjectIndustries.Dashboards.App.Services.Discord;
using ProjectIndustries.Dashboards.Core.Identity;
using ProjectIndustries.Dashboards.Core.Identity.Services;
using ProjectIndustries.Dashboards.Core.Primitives;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.Products.Services;
using ProjectIndustries.Dashboards.WebApi.Foundation.Authorization;

namespace ProjectIndustries.Dashboards.WebApi.Services
{
  public abstract class DiscordAuthenticationGrantValidatorBase : IExtensionGrantValidator
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserManager _userManager;
    private readonly IUserRepository _userRepository;
    protected readonly IDiscordClient DiscordClient;
    private readonly IUserProfileService _userProfileService;
    private readonly IDashboardManager _dashboardManager;
    private readonly IDashboardRepository _dashboardRepository;
    private readonly IMemberRoleProvider _memberRoleProvider;

    protected DiscordAuthenticationGrantValidatorBase(IDiscordClient discordClient, IUserManager userManager,
      IUserRepository userRepository, IUnitOfWork unitOfWork, IUserProfileService userProfileService,
      IDashboardManager dashboardManager, IDashboardRepository dashboardRepository,
      IMemberRoleProvider memberRoleProvider)
    {
      DiscordClient = discordClient;
      _userManager = userManager;
      _userRepository = userRepository;
      _unitOfWork = unitOfWork;
      _userProfileService = userProfileService;
      _dashboardManager = dashboardManager;
      _dashboardRepository = dashboardRepository;
      _memberRoleProvider = memberRoleProvider;
    }

    public async Task ValidateAsync(ExtensionGrantValidationContext context)
    {
      var payload = ExtractAuthPayload(context);
      var rawDashboardLocation = context.Request.Raw["dashboard"];
      if (string.IsNullOrEmpty(payload) || string.IsNullOrEmpty(rawDashboardLocation))
      {
        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
        return;
      }

      var dashboard = await _dashboardRepository.GetByRawLocationAsync(rawDashboardLocation);
      if (dashboard == null)
      {
        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
        return;
      }

      var securityToken = await AuthenticateAsync(dashboard.DiscordConfig.OAuthConfig, payload);
      if (securityToken == null)
      {
        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
        return;
      }

      var profile = await DiscordClient.GetProfileAsync(securityToken.AccessToken);
      if (profile == null)
      {
        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
        return;
      }

      var user = await _userRepository.GetByDiscordIdAsync(profile.Id);

      if (user == null)
      {
        user = User.CreateWithDiscordId(profile.Email, profile.Username, profile.Id, profile.Avatar,
          profile.Discriminator);

        var discordMember = await DiscordClient.GetGuildMemberAsync(dashboard.DiscordConfig, profile.Id);
        if (discordMember != null)
        {
          var roles = await DiscordClient.GetGuildRolesAsync(dashboard.DiscordConfig);
          user.ReplaceDiscordRoles(roles.Where(r => discordMember.Roles.Contains(r.Id))
            .Select(r => new DiscordRoleInfo
            {
              Id = r.Id,
              Name = r.Name,
              ColorHex = r.GetHexColor()
            }));
        }

        var error = await _userManager.CreateAsync(user);
        if (!string.IsNullOrEmpty(error))
        {
          context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, error);
          return;
        }
      }

      await DiscordClient.JoinGuildAsync(dashboard.DiscordConfig, securityToken.AccessToken, profile);
      if (dashboard.OwnerId != user.Id)
      {
        await _dashboardManager.TryJoinAsync(dashboard.Id, user.Id);
      }

      await _userProfileService.RefreshUserProfileIfOutdatedAsync(user.Id, dashboard);
      var claims = await AddDefaultClaimsAsync(securityToken, dashboard, user);
      await _unitOfWork.SaveEntitiesAsync();

      context.Result = new GrantValidationResult(user.Id.ToString(), GrantType, claims);
    }

    private async Task<List<Claim>> AddDefaultClaimsAsync(DiscordSecurityToken securityToken, Dashboard dashboard,
      User user)
    {
      var claims = new List<Claim>
      {
        new(AppClaimNames.DiscordAvatar, user.Avatar ?? ""),
        new(AppClaimNames.DiscordDiscriminator, user.Discriminator),
        new(AppClaimNames.DiscordAccessTokenToken, securityToken.AccessToken),
        new(AppClaimNames.DiscordRefreshTokenToken, securityToken.RefreshToken),
        new(AppClaimNames.CurrentDashboardId, dashboard.Id.ToString()),
        new(AppClaimNames.CurrentDashboardHostingMode, dashboard.HostingConfig.Mode.ToString()),
        new(AppClaimNames.CurrentDashboardDomain, dashboard.HostingConfig.DomainName),
      };

      var roles = await _memberRoleProvider.GetRolesAsync(dashboard.Id, user.Id);
      foreach (var role in roles)
      {
        claims.Add(new(AppClaimNames.RoleId, role.MemberRoleId.ToString()));
        claims.Add(new(AppClaimNames.RoleName, role.RoleName));
      }

      claims.AddRange(roles.SelectMany(_ => _.Permissions).Select(p => new Claim(AppClaimNames.Permission, p)));
      return claims;
    }

    public abstract string GrantType { get; }

    protected abstract string? ExtractAuthPayload(ExtensionGrantValidationContext context);
    protected abstract ValueTask<DiscordSecurityToken?> AuthenticateAsync(DiscordOAuthConfig config, string payload);
  }
}