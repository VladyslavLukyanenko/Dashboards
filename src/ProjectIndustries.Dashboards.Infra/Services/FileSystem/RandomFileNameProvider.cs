using System;
using ProjectIndustries.Dashboards.App.Config;
using ProjectIndustries.Dashboards.Core.FileStorage.Config;
using ProjectIndustries.Dashboards.Core.FileStorage.FileSystem;

namespace ProjectIndustries.Dashboards.Infra.Services.FileSystem
{
  public class RandomFileNameProvider
    : FileNameProvider
  {
    public RandomFileNameProvider(string? oldFileName = null)
      : base(oldFileName)
    {
    }

    protected override string GetDstFileNameWithoutExt(FileUploadsConfig cfg, IBinaryData data)
    {
      if (!cfg.GenerateRandomFileName)
      {
        throw new InvalidOperationException("Only random name generation supported. "
                                            + $"You should provide another implementation of {nameof(FileNameProvider)}");
      }

      return Guid.NewGuid().ToString("N");
    }
  }
}