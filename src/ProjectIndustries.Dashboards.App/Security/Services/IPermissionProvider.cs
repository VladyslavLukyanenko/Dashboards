using System.Collections.Generic;
using ProjectIndustries.Dashboards.App.Security.Model;

namespace ProjectIndustries.Dashboards.App.Security.Services
{
  public interface IPermissionProvider
  {
    IList<PermissionInfoData> GetSupportedPermissions();
  }
}