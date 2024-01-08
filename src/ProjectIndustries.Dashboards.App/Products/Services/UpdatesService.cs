using System;
using System.IO;
using System.Linq;
using CSharpFunctionalExtensions;
using ProjectIndustries.Dashboards.App.Config;

namespace ProjectIndustries.Dashboards.App.Products.Services
{
  public class UpdatesService : IUpdatesService
  {
    private readonly ArtifactsConfig _config;

    public UpdatesService(ArtifactsConfig config)
    {
      _config = config;
    }

    public Maybe<Version?> GetLatestVersion(Guid dashboardId, string product, string channel, string os)
    {
      var fullPath = Path.Combine(_config.BasePath, dashboardId.ToString(), product, channel, os);
      if (!Directory.Exists(fullPath))
      {
        return Maybe<Version?>.None;
      }

      return Directory.EnumerateFiles(fullPath)
        .Select(Path.GetFileNameWithoutExtension)
        .Select(f => Version.TryParse(f, out var v) ? v : null)
        .Where(v => v != null)
        .OrderByDescending(_ => _)
        .FirstOrDefault();
    }
  }
}