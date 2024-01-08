using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.App.Products.Model;
using ProjectIndustries.Dashboards.Core.Products;

namespace ProjectIndustries.Dashboards.App.Products.Services
{
  public interface IProductService
  {
    ValueTask<long> CreateAsync(ProductData data, CancellationToken ct = default);
    ValueTask UpdateAsync(Product product, ProductData data, CancellationToken ct = default);
  }
}