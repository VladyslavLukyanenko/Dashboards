using Microsoft.EntityFrameworkCore;
using ProjectIndustries.Dashboards.Core.Primitives;

namespace ProjectIndustries.Dashboards.Infra.Repositories
{
  public abstract class EfCrudRepository<T> : EfCrudRepository<T, long>
    where T : class, IEntity<long>, IEventSource
  {
    protected EfCrudRepository(DbContext context, IUnitOfWork unitOfWork)
      : base(context, unitOfWork)
    {
    }
  }
}