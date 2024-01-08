using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NodaTime;
using ProjectIndustries.Dashboards.App.Analytics.Model;
using ProjectIndustries.Dashboards.App.Analytics.Services;
using ProjectIndustries.Dashboards.Core.Products;

namespace ProjectIndustries.Dashboards.Infra.Analytics.Services
{
  public interface IDiscordInsightsClient
  {
    ValueTask<IList<JoinBySourceDiscordInsightsData>> GetJoinBySourceAsync(DiscordAnalyticsRequest request,
      DiscordConfig config, CancellationToken ct = default);

    ValueTask<IList<MembershipDiscordInsightsData>> GetMembershipAsync(DiscordAnalyticsRequest request,
      DiscordConfig config, CancellationToken ct = default);

    ValueTask<IList<LeaversDiscordInsightsData>> GetLeaversAsync(DiscordAnalyticsRequest request,
      DiscordConfig config, CancellationToken ct = default);

    ValueTask<IList<ActivationDiscordInsightsData>> GetActivationAsync(DiscordAnalyticsRequest request,
      DiscordConfig config, CancellationToken ct = default);

    ValueTask<IList<RetentionDiscordInsightsData>> GetRetentionAsync(DiscordAnalyticsRequest request,
      DiscordConfig config, CancellationToken ct = default);

    ValueTask<IList<JoinsDiscordInsightsData>> GetJoinsAsync(Instant start, Instant end,
      DiscordConfig config, CancellationToken ct = default);

    ValueTask<IList<OverviewDiscordInsightsData>> GetOverviewAsync(DiscordAnalyticsRequest request,
      DiscordConfig config, CancellationToken ct = default);

    ValueTask<IList<JoinsByInviteDiscordInsightsData>> GetJoinsByInviteAsync(DiscordAnalyticsRequest request,
      DiscordConfig config, CancellationToken ct = default);

    ValueTask<IList<JoinsByReferrerDiscordInsightsData>> GetJoinsByReferrerAsync(DiscordAnalyticsRequest request,
      DiscordConfig config, CancellationToken ct = default);
  }
}