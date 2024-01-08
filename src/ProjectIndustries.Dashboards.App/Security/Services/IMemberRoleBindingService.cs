using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ProjectIndustries.Dashboards.App.Security.Model;

namespace ProjectIndustries.Dashboards.App.Security.Services
{
  public interface IMemberRoleBindingService
  {
    ValueTask<Result> AssignRolesAsync(Guid dashboardId, IEnumerable<MemberRoleAssignmentData> data,
      CancellationToken ct = default);
  }
}