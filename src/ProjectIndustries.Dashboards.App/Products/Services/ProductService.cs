using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ProjectIndustries.Dashboards.App.Products.Config;
using ProjectIndustries.Dashboards.App.Products.Model;
using ProjectIndustries.Dashboards.Core.FileStorage.Config;
using ProjectIndustries.Dashboards.Core.FileStorage.FileSystem;
using ProjectIndustries.Dashboards.Core.Primitives;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.Products.Services;

namespace ProjectIndustries.Dashboards.App.Products.Services
{
  public class ProductService : IProductService
  {
    private readonly ProductConfig _config;
    private readonly IProductRepository _productRepository;
    private readonly IFileUploadService _fileUploadService;
    private readonly IMapper _mapper;

    public ProductService(ProductConfig config, IProductRepository productRepository,
      IFileUploadService fileUploadService, IMapper mapper)
    {
      _config = config;
      _productRepository = productRepository;
      _fileUploadService = fileUploadService;
      _mapper = mapper;
    }

    public async ValueTask<long> CreateAsync(ProductData data, CancellationToken ct = default)
    {
      var features = await CreateProductFeaturesAsync(data.Features, ct);

      var product = new Product(data.DashboardId, data.Name, data.Description, data.DownloadUrl, Slug.Create(data.Slug),
        Version.Parse(data.Version), data.DiscordRoleId, data.DiscordGuildId, data.CheckoutsTrackingWebhookUrl,
        features);

      await FillProductImagesAsync(product, data, ct);

      product = await _productRepository.CreateAsync(product, ct);

      return product.Id;
    }

    private async ValueTask<ProductFeature[]> CreateProductFeaturesAsync(IList<ProductFeatureData> featuresData,
      CancellationToken ct = default)
    {
      var featureTasks = featuresData.Select(async f =>
      {
        f.Icon = await _fileUploadService.UploadFileOrDefaultAsync(f.UploadedIcon, _config.FeatureIconUploadConfig,
          f.Icon, ct);

        return new ProductFeature(f.Icon, f.Title, f.Desc);
      });

      var features = await Task.WhenAll(featureTasks);
      return features;
    }

    private async ValueTask FillProductImagesAsync(Product product, ProductData data, CancellationToken ct = default)
    {
      var imageTasks = data.UploadedImages
        .Select(async f => await _fileUploadService.UploadFileOrDefaultAsync(f, _config.ImagesUploadConfig, "", ct));

      product.IconUrl = await _fileUploadService
        .UploadFileOrDefaultAsync(data.UploadedIcon, _config.IconUploadConfig, product.IconUrl, ct);

      product.ImageUrl = await _fileUploadService
        .UploadFileOrDefaultAsync(data.UploadedImage, _config.ImageUploadConfig, product.ImageUrl, ct);

      product.LogoUrl = await _fileUploadService
        .UploadFileOrDefaultAsync(data.UploadedLogo, _config.LogoUploadConfig, product.LogoUrl, ct);

      var images = await Task.WhenAll(imageTasks);
      foreach (var img in images)
      {
        product.Images.Add(img);
      }
    }

    public async ValueTask UpdateAsync(Product product, ProductData data, CancellationToken ct = default)
    {
      _mapper.Map(product, data);

      product.Features = await CreateProductFeaturesAsync(data.Features, ct);
      await FillProductImagesAsync(product, data, ct);

      _productRepository.Update(product);
    }

    private async ValueTask<string> UploadFileAsync(IBinaryData binaryData, FileUploadsConfig cfg, CancellationToken ct)
    {
      var result = await _fileUploadService.StoreAsync(binaryData, new[] {cfg}, ct: ct);
      return result.StoreFileResult.RelativeFilePath;
    }
  }
}