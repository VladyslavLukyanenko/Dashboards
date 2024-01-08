using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace ProjectIndustries.Dashboards.Core.Security.Services
{
  public interface IMemberRoleManager
  {
    ValueTask<Result<MemberRole>> CreateAsync(Guid dashboardId, string name, IEnumerable<string> permissions,
      decimal? salary, PayoutFrequency? payoutFrequency, Currency? currency, string? colorHex,
      CancellationToken ct = default);
  }
}