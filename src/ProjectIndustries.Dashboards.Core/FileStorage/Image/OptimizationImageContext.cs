using ProjectIndustries.Dashboards.Core.FileStorage.FileSystem;
using ProjectIndustries.Dashboards.Core.FileStorage.Image.ResizeStrategies;

namespace ProjectIndustries.Dashboards.Core.FileStorage.Image
{
  public class OptimizationImageContext
  {
    public OptimizationImageContext(IBinaryData image, ImageSize maxSize, ImageResizeStrategy resizeStrategy,
      bool resizeToFitExactSize, string? restrictedOutputImageType = null)
    {
      Image = image;
      MaxSize = maxSize;
      ResizeStrategy = resizeStrategy;
//            StorePath = storePath;
      ResizeToFitExactSize = resizeToFitExactSize;
      RestrictedOutputImageType = restrictedOutputImageType;
    }

    public string? RestrictedOutputImageType { get; }

    public IBinaryData Image { get; }
    public ImageSize MaxSize { get; }

    public ImageResizeStrategy ResizeStrategy { get; }

//        public string StorePath { get; }
    public bool ResizeToFitExactSize { get; }
    public bool ResizeEnabled => MaxSize != null;
  }
}