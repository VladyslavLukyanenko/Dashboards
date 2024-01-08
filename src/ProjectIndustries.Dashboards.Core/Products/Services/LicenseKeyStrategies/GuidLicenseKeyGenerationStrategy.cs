using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectIndustries.Dashboards.Core.Products.Services.LicenseKeyStrategies
{
  public class GuidLicenseKeyGenerationStrategy : ILicenseKeyGenerationStrategy
  {
    public bool IsSupported(LicenseKeyGeneratorConfig config) => config.Format == LicenseKeyFormat.Guid;

    public ValueTask<string> GenerateValueAsync(Plan plan, CancellationToken ct = default)
    {
      return new(Guid.NewGuid().ToString());
    }
  }
}