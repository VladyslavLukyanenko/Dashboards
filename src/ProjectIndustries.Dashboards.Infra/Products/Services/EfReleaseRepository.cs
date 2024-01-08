using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using ProjectIndustries.Dashboards.Core.Primitives;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.Products.Services;
using ProjectIndustries.Dashboards.Infra.Repositories;

namespace ProjectIndustries.Dashboards.Infra.Products.Services
{
  public class EfReleaseRepository : EfSoftRemovableCrudRepository<Release, long>, IReleaseRepository
  {
    private static readonly ConcurrentDictionary<long, SemaphoreSlim> DecrementGates = new();

    public EfReleaseRepository(DbContext context, IUnitOfWork unitOfWork)
      : base(context, unitOfWork)
    {
    }

    public async ValueTask<Release?> GetValidByPasswordAsync(string password, CancellationToken ct = default)
    {
      return await DataSource.FirstOrDefaultAsync(_ => _.Password == password && _.Stock > 0, ct);
    }

    public async ValueTask<Result> DecrementAsync(Release release, CancellationToken ct = default)
    {
      var gates = DecrementGates.GetOrAdd(release.Id, _ => new SemaphoreSlim(1, 1));
      await gates.WaitAsync(CancellationToken.None);
      try
      {
        do
        {
          var decrementResult = release.Decrement();
          if (decrementResult.IsFailure)
          {
            return decrementResult;
          }

          Update(release);
          try
          {
            if (await UnitOfWork.SaveEntitiesAsync(ct))
            {
              return Result.Success();
            }
          }
          catch (DbUpdateConcurrencyException)
          {
            // expected
            release = await DataSource.SingleAsync(_ => _.Id == release.Id, ct);
          }
        } while (true);
      }
      finally
      {
        gates.Release();
        DecrementGates.TryRemove(release.Id, out _);
      }
    }
  }
}