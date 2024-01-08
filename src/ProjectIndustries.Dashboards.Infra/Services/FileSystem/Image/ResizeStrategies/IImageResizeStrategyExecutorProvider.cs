using ProjectIndustries.Dashboards.Core.FileStorage.Image.ResizeStrategies;

namespace ProjectIndustries.Dashboards.Infra.Services.FileSystem.Image.ResizeStrategies
{
  public interface IImageResizeStrategyExecutorProvider
  {
    IImageResizeStrategyExecutor? Get(ImageResizeStrategy strategy);
  }
}