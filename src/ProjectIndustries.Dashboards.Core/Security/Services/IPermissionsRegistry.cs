using System.Collections.Generic;

namespace ProjectIndustries.Dashboards.Core.Security.Services
{
  public interface IPermissionsRegistry
  {
    IEnumerable<string> SupportedPermissions { get; }
    IReadOnlyList<string> GetPermissions(params string[] keyTokens);
  }
}