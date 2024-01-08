using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.App.Products.Model;

namespace ProjectIndustries.Dashboards.App.Products.Services
{
  public interface IProductProvider
  {
    ValueTask<IList<ProductData>> GetPurchasedByUserAsync(Guid dashboardId, long userId, CancellationToken ct = default);
    ValueTask<IList<ProductData>> GetAllAsync(Guid dashboardId, CancellationToken ct = default);
    ValueTask<ProductData?> GetBySlugAsync(string slug, CancellationToken ct = default);
    ValueTask<ProductData?> GetByIdAsync(long productId, CancellationToken ct = default);
  }
}