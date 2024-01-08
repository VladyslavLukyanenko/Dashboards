using ProjectIndustries.Dashboards.App.Config;
using ProjectIndustries.Dashboards.Core.FileStorage.Config;
using ProjectIndustries.Dashboards.Core.FileStorage.FileSystem;

namespace ProjectIndustries.Dashboards.Infra.Services.FileSystem
{
  public interface IBinaryDataProcessingPipelineProvider
  {
    IBinaryDataProcessingPipeline? GetPipeline(FileUploadsConfig cfg, IBinaryData data);
  }
}