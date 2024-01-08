using ProjectIndustries.Dashboards.WebApi.Foundation.Authorization;

// ReSharper disable once CheckNamespace
namespace ProjectIndustries.Dashboards.WebApi.Authorization
{
  public partial class Permissions
  {
    [PermissionDescription("Licenses suspend")]
    public const string LicenseKeysToggleSuspend = nameof(LicenseKeysToggleSuspend);

    [PermissionDescription("Licenses stats")]
    public const string LicenseKeysStatsUsedCount = nameof(LicenseKeysStatsUsedCount);

    [PermissionDescription("Products create/edit")]
    public const string ProductsWrite = nameof(ProductsWrite);
  }
}