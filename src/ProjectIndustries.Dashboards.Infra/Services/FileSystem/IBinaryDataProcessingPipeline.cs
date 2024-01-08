using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.App.Config;
using ProjectIndustries.Dashboards.Core.FileStorage.Config;
using ProjectIndustries.Dashboards.Core.FileStorage.FileSystem;

namespace ProjectIndustries.Dashboards.Infra.Services.FileSystem
{
  public interface IBinaryDataProcessingPipeline
  {
    bool CanProcess(FileUploadsConfig fileCfg, IBinaryData data);

    Task<IBinaryData> ProcessAsync(FileUploadsConfig fileCfg, IBinaryData data, CancellationToken token = default);
  }
}