using ProjectIndustries.Dashboards.Core.FileStorage.Image;
using ProjectIndustries.Dashboards.Core.FileStorage.Image.ResizeStrategies;
using SkiaSharp;

namespace ProjectIndustries.Dashboards.Infra.Services.FileSystem.Image
{
  public interface IImageResizeStrategyExecutor
  {
    ImageResizeStrategy Strategy { get; }
    SKBitmap Execute(OptimizationImageContext context, SKBitmap convertedBitmap);
  }
}