using System;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.Primitives;

namespace ProjectIndustries.Dashboards.Core.Products.Services
{
  public interface IProductRepository : ICrudRepository<Product>
  {
    ValueTask<Version?> GetProductVersionAsync(long productId, CancellationToken ct = default);
  }
}