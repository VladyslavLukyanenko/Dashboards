using System;
using System.Collections.Generic;
using System.Linq;
using ProjectIndustries.Dashboards.Core.Products.Services.LicenseKeyStrategies;

namespace ProjectIndustries.Dashboards.Core.Products.Services
{
  public class IoCBasedLicenseKeyGenerationStrategyProvider : ILicenseKeyGenerationStrategyProvider
  {
    private readonly IEnumerable<ILicenseKeyGenerationStrategy> _strategies;

    public IoCBasedLicenseKeyGenerationStrategyProvider(IEnumerable<ILicenseKeyGenerationStrategy> strategies)
    {
      _strategies = strategies;
    }

    public ILicenseKeyGenerationStrategy GetGeneratorFor(LicenseKeyGeneratorConfig config) =>
      _strategies.FirstOrDefault(_ => _.IsSupported(config))
      ?? throw new InvalidOperationException("Can't find license key generation strategy for format " + config.Format);
  }
}