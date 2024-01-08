using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.FileStorage.FileSystem;

namespace ProjectIndustries.Dashboards.Core.FileStorage.Image
{
  public interface IImageOptimizationService
  {
    Task<IBinaryData> OptimizeAsync(OptimizationImageContext context, CancellationToken token = default);
  }
}