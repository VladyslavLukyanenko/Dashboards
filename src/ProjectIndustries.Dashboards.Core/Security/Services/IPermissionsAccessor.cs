using System.Collections.Generic;

namespace ProjectIndustries.Dashboards.Core.Security.Services
{
  public interface IPermissionsAccessor
  {
    IReadOnlyList<string> CurrentRequiredPermissions { get; }
  }
}