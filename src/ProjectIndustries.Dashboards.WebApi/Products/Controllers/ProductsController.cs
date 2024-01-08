using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectIndustries.Dashboards.App.Products.Model;
using ProjectIndustries.Dashboards.App.Products.Services;
using ProjectIndustries.Dashboards.Core.Products.Services;
using ProjectIndustries.Dashboards.WebApi.Authorization;
using ProjectIndustries.Dashboards.WebApi.Foundation.Authorization;
using ProjectIndustries.Dashboards.WebApi.Foundation.Model;
using ProjectIndustries.Dashboards.WebApi.Foundation.Mvc.Controllers;

namespace ProjectIndustries.Dashboards.WebApi.Products.Controllers
{
  public class ProductsController : SecuredDashboardBoundControllerBase
  {
    private readonly IProductProvider _productProvider;
    private readonly IProductService _productService;
    private readonly IProductRepository _productRepository;

    public ProductsController(IServiceProvider provider, IProductProvider productProvider,
      IProductService productService, IProductRepository productRepository)
      : base(provider)
    {
      _productProvider = productProvider;
      _productService = productService;
      _productRepository = productRepository;
    }

    // by dashboardId
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<ProductData>))]
    public async ValueTask<IActionResult> GetAllAsync(CancellationToken ct)
    {
      var list = await _productProvider.GetAllAsync(CurrentDashboardId, ct);
      return Ok(list);
    }

    // +authorize dashboard member or admin
    [HttpGet("{slug}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<ProductData>))]
    public async ValueTask<IActionResult> GetBySlugAsync(string slug, CancellationToken ct)
    {
      var product = await _productProvider.GetBySlugAsync(slug, ct);
      if (product == null)
      {
        return NotFound();
      }

      await AppAuthorizationService.AdminOrMemberAsync(product.DashboardId)
        .OrThrowForbid();

      return Ok(product);
    }

    [HttpGet("@me")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<IList<ProductData>>))]
    public async ValueTask<IActionResult> GetListByUserIdAsync(CancellationToken ct)
    {
      var list = await _productProvider.GetPurchasedByUserAsync(CurrentDashboardId, CurrentUserId, ct);
      return Ok(list);
    }

    // +authorize dashboard owner or admin
    [HttpPost]
    [AuthorizePermission(Permissions.ProductsWrite)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<long>))]
    public async ValueTask<IActionResult> CreateAsync([FromBody] ProductData data, CancellationToken ct)
    {
      await AppAuthorizationService.AuthorizeCurrentPermissionsAsync(data.DashboardId)
        .OrThrowForbid();

      var createdId = await _productService.CreateAsync(data, ct);
      return Ok(createdId);
    }

    // +authorize dashboard owner or admin
    [HttpPut("{productId:long}")]
    [AuthorizePermission(Permissions.ProductsWrite)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> UpdateAsync(long productId, [FromBody] ProductData data, CancellationToken ct)
    {
      var product = await _productRepository.GetByIdAsync(productId, ct);
      if (product == null)
      {
        return NotFound();
      }

      await AppAuthorizationService.AuthorizeCurrentPermissionsAsync(product.DashboardId)
        .OrThrowForbid();

      await _productService.UpdateAsync(product, data, ct);
      return NoContent();
    }


    // +authorize dashboard owner or admin
    [HttpDelete("{productId:long}")]
    [AuthorizePermission(Permissions.ProductsWrite)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> RemoveAsync(long productId, CancellationToken ct)
    {
      var product = await _productRepository.GetByIdAsync(productId, ct);
      if (product == null)
      {
        return NotFound();
      }

      await AppAuthorizationService.AuthorizeCurrentPermissionsAsync(product.DashboardId)
        .OrThrowForbid();

      _productRepository.Remove(product);
      return NoContent();
    }
  }
}