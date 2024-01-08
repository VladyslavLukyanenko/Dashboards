using System.Threading;
using System.Threading.Tasks;

namespace ProjectIndustries.Dashboards.Core.Products.Services
{
  public interface ILicenseKeyScheduler
  {
    ValueTask ScheduleKeyRemovalAsync(LicenseKey key, CancellationToken ct = default);
  }
}