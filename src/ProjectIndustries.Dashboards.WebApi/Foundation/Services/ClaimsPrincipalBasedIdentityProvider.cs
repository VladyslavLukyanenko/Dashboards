using Microsoft.AspNetCore.Http;
using ProjectIndustries.Dashboards.Core.Identity.Services;
using ProjectIndustries.Dashboards.Core.Services;
using ProjectIndustries.Dashboards.WebApi.Foundation.Authorization;

namespace ProjectIndustries.Dashboards.WebApi.Foundation.Services
{
  public class ClaimsPrincipalBasedIdentityProvider : IIdentityProvider
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClaimsPrincipalBasedIdentityProvider(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public long? GetCurrentIdentity()
    {
      return _httpContextAccessor.HttpContext?.User.GetUserId();
    }
  }
}