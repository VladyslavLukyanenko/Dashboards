using System;
using NodaTime;
using ProjectIndustries.Dashboards.Core.Primitives;
using ProjectIndustries.Dashboards.Core.Products.Events.Dashboards;

namespace ProjectIndustries.Dashboards.Core.Products
{
  public class Dashboard : SoftRemovableEntity<Guid>
  {
    private bool _chargeBackersExportEnabled;

    private Dashboard()
    {
    }

    public Dashboard(string name, long ownerId, ulong guildId, string discordAccessToken, string discordAuthorizeUrl)
    {
      Name = name;
      OwnerId = ownerId;
      DiscordConfig.GuildId = guildId;
      DiscordConfig.BotAccessToken = discordAccessToken;
      DiscordConfig.OAuthConfig.AuthorizeUrl = discordAuthorizeUrl;
      TimeZoneId = DateTimeZone.Utc.Id /*"Etc/GMT"*/;
    }

    public string Name { get; set; } = null!;

    public StripeIntegrationConfig StripeConfig { get; private set; } = null!;
    public long OwnerId { get; private set; }
    public Instant? ExpiresAt { get; private set; }
    public DiscordConfig DiscordConfig { get; private set; } = new();
    public string? LogoSrc { get; set; }
    public string? CustomBackgroundSrc { get; set; }
    public string TimeZoneId { get; set; } = null!;
    public HostingConfig HostingConfig { get; private set; } = new();

    public bool ChargeBackersExportEnabled
    {
      get => _chargeBackersExportEnabled;
      set
      {
        if (value == _chargeBackersExportEnabled)
        {
          return;
        }

        _chargeBackersExportEnabled = value;
        AddDomainEvent(new ChargeBackersExportToggled(Id, value));
      }
    }
  }
}