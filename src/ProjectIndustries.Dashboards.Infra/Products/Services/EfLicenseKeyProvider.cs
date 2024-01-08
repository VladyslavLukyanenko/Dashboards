using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using ProjectIndustries.Dashboards.App.Identity.Model;
using ProjectIndustries.Dashboards.App.Products.Model;
using ProjectIndustries.Dashboards.App.Products.Services;
using ProjectIndustries.Dashboards.Core.Collections;
using ProjectIndustries.Dashboards.Core.Identity;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Infra.Services;

namespace ProjectIndustries.Dashboards.Infra.Products.Services
{
  public class EfLicenseKeyProvider : DataProvider, ILicenseKeyProvider
  {
    private readonly IQueryable<LicenseKey> _aliveLicenseKeys;
    private readonly IQueryable<User> _users;
    private readonly IQueryable<Plan> _plans;
    private readonly IQueryable<Release> _releases;
    private readonly IQueryable<JoinedDashboard> _joinedDashboards;
    private readonly IMapper _mapper;

    public EfLicenseKeyProvider(DbContext context, IMapper mapper)
      : base(context)
    {
      _aliveLicenseKeys = GetAliveDataSource<LicenseKey>();
      _users = GetDataSource<User>();
      _plans = GetDataSource<Plan>();
      _releases = GetDataSource<Release>();
      _joinedDashboards = GetDataSource<JoinedDashboard>();
      _mapper = mapper;
    }

    public async ValueTask<IPagedList<LicenseKeyShortData>> GetPageByReleaseIdAsync(long releaseId,
      PageRequest pageRequest, CancellationToken ct = default)
    {
      var query = from l in _aliveLicenseKeys
        join u in _users on l.UserId equals u.Id into utmp
        from u in utmp.DefaultIfEmpty()
        where l.ReleaseId == releaseId
        orderby l.CreatedAt descending, l.Id
        select new LicenseKeyShortData
        {
          Id = l.Id,
          Value = l.Value,
          User = u == null
            ? null
            : new UserRef
            {
              Id = u.Id,
              Picture = u.Avatar,
              FullName = u.Name + "#" + u.Discriminator
            }
        };


      return await query.PaginateAsync(pageRequest, ct);
    }

    public async ValueTask<IPagedList<LicenseKeySnapshotData>> GetPageAsync(Guid dashboardId,
      LicenseKeyPageRequest pageRequest,
      CancellationToken ct = default)
    {
      var searchTerm = pageRequest.NormalizeSearchTerm();
      // var allKeys = GetDataSource<LicenseKey>();

      var query = from licenceKey in _aliveLicenseKeys
        join user in _users on licenceKey.UserId equals user.Id
          into utmp
        from user in utmp.DefaultIfEmpty()
        join plan in _plans on licenceKey.PlanId equals plan.Id
        join release in _releases on licenceKey.ReleaseId equals release.Id
          into tmp
        from release in tmp.DefaultIfEmpty()
        orderby licenceKey.RemovedAt == Instant.MaxValue descending, licenceKey.CreatedAt, licenceKey.Value
        where
          !pageRequest.LifetimeOnly.HasValue || licenceKey.Expiry.HasValue != pageRequest.LifetimeOnly
          && (
            user.Email.Value.Contains(searchTerm)
            || user.DiscordId.ToString().Contains(searchTerm)
            || licenceKey.Value.Contains(searchTerm)
          )
        where licenceKey.DashboardId == dashboardId
        select new DenormalizedLicenseKey
        {
          Key = licenceKey,
          Plan = plan,
          User = user,
          Release = release
        };

      if (pageRequest.PlanId.HasValue)
      {
        query = query.Where(_ => _.Plan.Id == pageRequest.PlanId);
      }

      if (pageRequest.ReleaseId.HasValue)
      {
        query = query.Where(_ => _.Release != null && _.Release.Id == pageRequest.ReleaseId);
      }

      if (pageRequest.SortBy.HasValue)
      {
        switch (pageRequest.SortBy.Value)
        {
          case LicensesSortBy.Newest:
            query = query.OrderByDescending(_ => _.Key.CreatedAt);
            break;
          case LicensesSortBy.Oldest:
            query = query.OrderBy(_ => _.Key.CreatedAt);
            break;
          case LicensesSortBy.Expiry:
            query = query.Where(_ => _.Key.Expiry.HasValue).OrderByDescending(_ => _.Key.Expiry);
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }

      return await query.ProjectTo<LicenseKeySnapshotData>(_mapper.ConfigurationProvider)
        .PaginateAsync(pageRequest, ct);
    }

    public async ValueTask<IList<LicenseKeySnapshotData>> GetAllByProductIdAsync(long userId, long productId,
      CancellationToken ct)
    {
      var licenseKeys = await _aliveLicenseKeys.Where(_ => _.UserId == userId && _.ProductId == productId)
        .ToArrayAsync(ct);
      return _mapper.Map<IList<LicenseKeySnapshotData>>(licenseKeys);
    }

    public async ValueTask<LicenseKeySummaryData> GetSummaryByIdAsync(long id, CancellationToken ct = default)
    {
      var query = from l in _aliveLicenseKeys
        join u in _users on l.UserId equals u.Id into utmp
        from u in utmp.DefaultIfEmpty()
        join jd in _joinedDashboards
          on new {UserId = l.UserId.Value, l.DashboardId} equals new {jd.UserId, jd.DashboardId}
          into jtmp
        from jd in jtmp.DefaultIfEmpty()
        select new LicenseKeySummaryData
        {
          Id = l.Id,
          Expiry = l.Expiry,
          Reason = l.Reason,
          Value = l.Value,
          PlanId = l.PlanId,
          UnbindableAfter = l.UnbindableAfter,
          TrialEndsAt = l.TrialEndsAt,
          User = u == null
            ? null
            : new LicenseKeySummaryData.KeyOwnerSummaryData
            {
              Id = u.Id,
              Picture = u.Avatar,
              FullName = u.Name + "#" + u.Discriminator,
              JoinedAt = jd.JoinedAt
            }
        };

      return await query.FirstOrDefaultAsync(_ => _.Id == id, ct);
    }

    public async ValueTask<int> GetUsedTodayCountAsync(Guid dashboardId, Offset offset, CancellationToken ct = default)
    {
      var now = SystemClock.Instance.GetCurrentInstant()
        .WithOffset(offset);

      var from = now.With(_ => LocalTime.Midnight).ToInstant();
      var until = now.With(_ => LocalTime.Noon.PlusMilliseconds(-1)).ToInstant();

      return await _aliveLicenseKeys.CountAsync(_ =>
        _.DashboardId == dashboardId
        && _.LastAuthRequest.HasValue && _.LastAuthRequest.Value >= from && _.LastAuthRequest.Value <= until, ct);
    }
  }

  public class DenormalizedLicenseKey
  {
    public LicenseKey Key { get; set; } = null!;
    public Plan Plan { get; set; } = null!;
    public Release? Release { get; set; }
    public User? User { get; set; }
  }
}