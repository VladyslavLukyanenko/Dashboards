using System;
using CSharpFunctionalExtensions;

namespace ProjectIndustries.Dashboards.App.Products.Services
{
  public interface IUpdatesService
  {
    Maybe<Version?> GetLatestVersion(Guid dashboardId, string product, string channel, string os);
  }
}