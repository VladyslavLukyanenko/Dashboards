using Microsoft.AspNetCore.Authorization;

namespace ProjectIndustries.Dashboards.WebApi.Authorization
{
  public class AdminOrSameUserRequirement : IAuthorizationRequirement
  {
    public AdminOrSameUserRequirement(long userId)
    {
      UserId = userId;
    }

    public long UserId { get; }
  }
}