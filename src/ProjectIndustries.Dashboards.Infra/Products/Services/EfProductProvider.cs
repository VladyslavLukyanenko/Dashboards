using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectIndustries.Dashboards.App.Products.Model;
using ProjectIndustries.Dashboards.App.Products.Services;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Infra.Services;

namespace ProjectIndustries.Dashboards.Infra.Products.Services
{
  public class EfProductProvider : DataProvider, IProductProvider
  {
    private readonly IMapper _mapper;
    private readonly IQueryable<Product> _products;
    private readonly IQueryable<LicenseKey> _licenseKeys;

    public EfProductProvider(DbContext context, IMapper mapper) : base(context)
    {
      _mapper = mapper;
      _products = GetDataSource<Product>();
      _licenseKeys = GetDataSource<LicenseKey>();
    }

    public async ValueTask<IList<ProductData>> GetAllAsync(Guid dashboardId, CancellationToken ct = default)
    {
      var entities = await _products.Where(p => p.DashboardId == dashboardId).ToArrayAsync(ct);
      return _mapper.Map<IList<ProductData>>(entities);
    }

    public async ValueTask<ProductData?> GetBySlugAsync(string slug, CancellationToken ct = default)
    {
      var entity = await _products.FirstOrDefaultAsync(p => p.Slug.Value == slug, ct);
      return _mapper.Map<ProductData>(entity);
    }

    public async ValueTask<ProductData?> GetByIdAsync(long productId, CancellationToken ct = default)
    {
      var entity = await _products.FirstOrDefaultAsync(p => p.Id == productId, ct);
      return _mapper.Map<ProductData>(entity);
    }

    public async ValueTask<IList<ProductData>> GetPurchasedByUserAsync(Guid dashboardId, long userId, CancellationToken ct = default)
    {
      var productsIds = _licenseKeys
        .Where(_ => _.DashboardId == dashboardId)
        .Where(_ => _.UserId == userId)
        .Select(_ => _.ProductId)
        .Distinct();
      var entities = await _products.Where(_ => productsIds.Contains(_.Id)).ToArrayAsync(ct);
      return _mapper.Map<IList<ProductData>>(entities);
    }
  }
}