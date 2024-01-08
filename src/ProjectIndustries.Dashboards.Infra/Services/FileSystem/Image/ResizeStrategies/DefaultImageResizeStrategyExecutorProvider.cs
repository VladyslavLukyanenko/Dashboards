using System.Collections.Generic;
using System.Linq;
using ProjectIndustries.Dashboards.Core.FileStorage.Image.ResizeStrategies;

namespace ProjectIndustries.Dashboards.Infra.Services.FileSystem.Image.ResizeStrategies
{
  public class DefaultImageResizeStrategyExecutorProvider
    : IImageResizeStrategyExecutorProvider
  {
    private readonly IEnumerable<IImageResizeStrategyExecutor> _executors;

    public DefaultImageResizeStrategyExecutorProvider(IEnumerable<IImageResizeStrategyExecutor> executors)
    {
      _executors = executors;
    }

    public IImageResizeStrategyExecutor? Get(ImageResizeStrategy strategy)
    {
      return _executors.FirstOrDefault(_ => _.Strategy == strategy);
    }
  }
}