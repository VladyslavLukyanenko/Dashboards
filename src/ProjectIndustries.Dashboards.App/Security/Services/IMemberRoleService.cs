using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ProjectIndustries.Dashboards.App.Security.Model;
using ProjectIndustries.Dashboards.Core.Security;

namespace ProjectIndustries.Dashboards.App.Security.Services
{
  public interface IMemberRoleService
  {
    ValueTask<Result<MemberRole>> CreateAsync(Guid dashboardId, MemberRoleData data, CancellationToken ct = default);
    ValueTask UpdateAsync(MemberRole role, MemberRoleData data, CancellationToken ct = default);
  }
}