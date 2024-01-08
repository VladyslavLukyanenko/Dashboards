using ProjectIndustries.Dashboards.Core.FileStorage.Config;

namespace ProjectIndustries.Dashboards.Core.FileStorage.FileSystem
{
  public interface IFileNameProvider
  {
    string? OldFileName { get; }
    string GetDstFileName(FileUploadsConfig cfg, IBinaryData data, string? computedHash = null);
  }
}