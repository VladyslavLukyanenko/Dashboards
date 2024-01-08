using System.Threading;
using System.Threading.Tasks;

namespace ProjectIndustries.Dashboards.Core.Products.Services
{
  public interface IStatsService
  {
    ValueTask RemoveByLicenseKeyIdAsync(long keyId, CancellationToken ct = default);
  }

  public class FakeStatsService : IStatsService
  {
    public ValueTask RemoveByLicenseKeyIdAsync(long keyId, CancellationToken ct = default)
    {
      throw new System.NotImplementedException();
    }
  }
}