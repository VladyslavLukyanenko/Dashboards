using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ProjectIndustries.Dashboards.App.Analytics.Model;

namespace ProjectIndustries.Dashboards.App.Analytics.Services
{
  public interface IAnalyticsProvider
  {
    ValueTask<Result<GeneralAnalytics>> GetGeneralAnalyticsAsync(Guid dashboardId, GeneralAnalyticsRequest request,
      CancellationToken ct = default);
  }
}