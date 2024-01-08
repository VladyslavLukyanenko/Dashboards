using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectIndustries.Dashboards.Core.Analytics.Services
{
  public interface IUserSessionRepository
  {
    ValueTask<UserSession?> GetByIdAsync(Guid sessionId, CancellationToken ct = default);
    ValueTask<UserSession> CreateAsync(UserSession userSession, CancellationToken ct = default);
    UserSession Update(UserSession session);
  }
}