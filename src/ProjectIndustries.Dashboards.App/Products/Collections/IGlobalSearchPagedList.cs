using ProjectIndustries.Dashboards.App.Products.Model;
using ProjectIndustries.Dashboards.Core.Collections;

namespace ProjectIndustries.Dashboards.App.Products.Collections
{
  public interface IGlobalSearchPagedList : IPagedList<GlobalSearchResult>
  {
    int LicensesCount { get; }
    int ReleasesCount { get; }
  }
}