using ProjectIndustries.Dashboards.Core.FileStorage.Image;
using ProjectIndustries.Dashboards.Core.FileStorage.Image.ResizeStrategies;
using SkiaSharp;

namespace ProjectIndustries.Dashboards.Infra.Services.FileSystem.Image.ResizeStrategies
{
  public class ImageResizeStrategyByWidthExecutor
    : ImageResizeStrategyByWidthHeightExecutor
  {
    public override ImageResizeStrategy Strategy => ImageResizeStrategy.ByWidth;

    protected override ImageResizeBy ResizeBy => ImageResizeBy.Width;

    protected override int GetDestinationValue(ImageSize size)
    {
      return size.Width;
    }

    protected override bool ResizeRequired(SKBitmap convertedBitmap, ImageSize size, OptimizationImageContext context)
    {
      if (context.ResizeToFitExactSize)
      {
        return convertedBitmap.Width != size.Width;
      }

      return convertedBitmap.Width > size.Width;
    }
  }
}