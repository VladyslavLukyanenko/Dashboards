using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectIndustries.Dashboards.Core.Primitives;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.Products.Services;
using ProjectIndustries.Dashboards.Infra.Repositories;

namespace ProjectIndustries.Dashboards.Infra.Products.Services
{
  public class EfProductRepository : EfSoftRemovableCrudRepository<Product, long>, IProductRepository
  {
    public EfProductRepository(DbContext context, IUnitOfWork unitOfWork)
      : base(context, unitOfWork)
    {
    }

    public async ValueTask<Version?> GetProductVersionAsync(long productId, CancellationToken ct = default)
    {
      return await DataSource.Where(_ => _.Id == productId).Select(_ => _.Version).FirstOrDefaultAsync(ct);
    }
  }
}