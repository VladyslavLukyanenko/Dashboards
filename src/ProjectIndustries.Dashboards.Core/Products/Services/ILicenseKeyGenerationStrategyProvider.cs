using ProjectIndustries.Dashboards.Core.Products.Services.LicenseKeyStrategies;

namespace ProjectIndustries.Dashboards.Core.Products.Services
{
  public interface ILicenseKeyGenerationStrategyProvider
  {
    ILicenseKeyGenerationStrategy GetGeneratorFor(LicenseKeyGeneratorConfig config);
  }
}