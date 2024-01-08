using ProjectIndustries.Dashboards.App.Services;

namespace ProjectIndustries.Dashboards.Infra
{
  // ReSharper disable once InconsistentNaming
  public static class IPathsServiceExtensions
  {
    public static string? GetAbsoluteImageUrl(this IPathsService pathsService, string? relativePath,
      string? fallbackRelativePath)
    {
      if (string.IsNullOrEmpty(relativePath))
      {
        relativePath = fallbackRelativePath;
      }

      return pathsService.ToAbsoluteUrl(relativePath);
    }
  }
}