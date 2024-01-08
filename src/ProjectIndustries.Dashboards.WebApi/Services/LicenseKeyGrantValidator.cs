using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using ProjectIndustries.Dashboards.App.Products.Services;
using ProjectIndustries.Dashboards.Core.Primitives;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.Products.Services;
using ProjectIndustries.Dashboards.WebApi.Foundation.Authorization;

namespace ProjectIndustries.Dashboards.WebApi.Services
{
  public class LicenseKeyGrantValidator : IExtensionGrantValidator
  {
    public const string GrantTypeName = "license-key";
    private readonly ILicenseKeyRepository _licenseKeyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserProfileService _userProfileService;
    private readonly IProductRepository _productRepository;

    public LicenseKeyGrantValidator(ILicenseKeyRepository licenseKeyRepository, IUnitOfWork unitOfWork,
      IUserProfileService userProfileService, IProductRepository productRepository)
    {
      _licenseKeyRepository = licenseKeyRepository;
      _unitOfWork = unitOfWork;
      _userProfileService = userProfileService;
      _productRepository = productRepository;
    }

    public async Task ValidateAsync(ExtensionGrantValidationContext context)
    {
      var keyValue = context.Request.Raw["key"];
      var hwid = context.Request.Raw["hwid"];
      var productIdRaw = context.Request.Raw["pid"];
      if (string.IsNullOrEmpty(keyValue) || string.IsNullOrEmpty(hwid) || string.IsNullOrEmpty(productIdRaw)
          || !long.TryParse(productIdRaw, out var productId))
      {
        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest);
        return;
      }

      LicenseKey? key = await _licenseKeyRepository.GetByValueAsync(keyValue);
      if (key == null)
      {
        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest);
        return;
      }

      if (key.IsExpired())
      {
        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "Key is expired");
        return;
      }

      if (key.IsSuspended())
      {
        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "Key is suspended");
        return;
      }

      if (!key.IsUnbound())
      {
        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "Key is unbound");
        return;
      }

      if (productId != key.ProductId)
      {
        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "Invalid key provided");
        return;
      }

      if (!key.IsSessionValid(hwid))
      {
        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "Machine mismatch");
        return;
      }

      key.BindSessionIfEmpty(hwid);
      key.RefreshActivity();
      _licenseKeyRepository.Update(key);
      await _userProfileService.RefreshUserProfileIfOutdatedAsync(key.UserId!.Value, key.DashboardId);
      await _unitOfWork.SaveEntitiesAsync();

      var claims = new List<Claim>
      {
        new(AppClaimNames.LicenseKey, key.Value),
        new(AppClaimNames.ProductVersion, (await _productRepository.GetProductVersionAsync(key.ProductId))?.ToString()!)
      };

      var absoluteExpiry = key.CalculateAbsoluteExpiry();
      if (absoluteExpiry.HasValue)
      {
        claims.Add(new Claim(AppClaimNames.LicenseKeyExpiry, absoluteExpiry.Value.ToString()));
      }

      context.Result = new GrantValidationResult(key.UserId.ToString(), GrantType, claims);
    }

    public string GrantType => GrantTypeName;
  }
}