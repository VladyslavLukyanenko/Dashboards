namespace ProjectIndustries.Dashboards.Core.Identity.Services
{
  public interface IIdentityProvider
  {
    long? GetCurrentIdentity();
  }
}