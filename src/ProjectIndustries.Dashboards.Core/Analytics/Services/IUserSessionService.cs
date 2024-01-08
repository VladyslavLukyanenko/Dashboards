using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectIndustries.Dashboards.Core.Analytics.Services
{
  public interface IUserSessionService
  {
    ValueTask<Guid> RefreshOrCreateSessionAsync(Guid dashboardId, Guid? sessionId, string userAgent,
      IPAddress? ipAddress, long? userId, CancellationToken ct = default);
  }
}