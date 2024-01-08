using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ProjectIndustries.Dashboards.Core.Primitives;

namespace ProjectIndustries.Dashboards.Core.Products.Services
{
  public interface IReleaseRepository : ICrudRepository<Release>
  {
    ValueTask<Release?> GetValidByPasswordAsync(string password, CancellationToken ct = default);
    ValueTask<Result> DecrementAsync(Release release, CancellationToken ct = default);
  }
}