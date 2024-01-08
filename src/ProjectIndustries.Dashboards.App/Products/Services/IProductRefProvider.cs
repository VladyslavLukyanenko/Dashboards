using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.App.Products.Model;

namespace ProjectIndustries.Dashboards.App.Products.Services
{
  public interface IProductRefProvider
  {
    ValueTask<ProductRef> GetRefAsync(long productId, CancellationToken ct = default);
  }
}