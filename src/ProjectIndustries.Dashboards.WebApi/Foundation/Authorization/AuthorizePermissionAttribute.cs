using Microsoft.AspNetCore.Authorization;

namespace ProjectIndustries.Dashboards.WebApi.Foundation.Authorization
{
  public class AuthorizePermissionAttribute : AuthorizeAttribute
  {
    public AuthorizePermissionAttribute(string permission)
    {
      Permission = permission;
    }

    public string Permission { get; }
  }
}