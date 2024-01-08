using System.Linq;
using NodaTime;
using ProjectIndustries.Dashboards.Core.Primitives;

namespace ProjectIndustries.Dashboards.Infra
{
  // ReSharper disable once InconsistentNaming
  public static class IQueryableExtensions
  {
    public static IQueryable<T> WhereNotRemoved<T>(this IQueryable<T> src)
      where T: class, ISoftRemovable
    {
      return src.Where(_ => _.RemovedAt == Instant.MaxValue);
    }
    
  }
}