using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ProjectIndustries.Dashboards.App.Security.Model;
using ProjectIndustries.Dashboards.App.Security.Services;
using ProjectIndustries.Dashboards.Core.Security.Services;
using ProjectIndustries.Dashboards.WebApi.Authorization;
using ProjectIndustries.Dashboards.WebApi.Foundation.Authorization;

namespace ProjectIndustries.Dashboards.WebApi.Security
{
  public class PermissionProvider : IPermissionProvider
  {
    private static readonly IDictionary<string, string> PermissionTranslations;

    private readonly IPermissionsRegistry _permissionsRegistry;

    static PermissionProvider()
    {
      PermissionTranslations = typeof(Permissions)
        .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
        .Where(_ => _.IsLiteral && !_.IsInitOnly)
        .ToDictionary(_ => (string) _.GetValue(null)!,
          _ => _.GetCustomAttribute<PermissionDescriptionAttribute>()?.Description ?? (string) _.GetValue(null)!);
    }
    
    public PermissionProvider(IPermissionsRegistry permissionsRegistry)
    {
      _permissionsRegistry = permissionsRegistry;
    }

    public IList<PermissionInfoData> GetSupportedPermissions()
    {
      return _permissionsRegistry.SupportedPermissions.Select(p => new PermissionInfoData
        {
          Permission = p,
          Description = PermissionTranslations.ContainsKey(p) ? PermissionTranslations[p] : p
        })
        .OrderBy(_ => _.Permission)
        .ToList();
    }
  }
}