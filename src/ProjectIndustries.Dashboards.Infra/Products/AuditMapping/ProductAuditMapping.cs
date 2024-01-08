using ProjectIndustries.Dashboards.Core.Audit.Mappings;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Infra.Audit.EntryValueConverters;

namespace ProjectIndustries.Dashboards.Infra.Products.AuditMapping
{
  public class ProductAuditMapping : AuditMappingBase
  {
    public ProductAuditMapping()
    {
      Map<Product, long>()
        .Property(_ => _.Name)
        .Property(_ => _.Description)
        .Property(_ => _.Slug.Value)
        .Property(_ => _.DiscordGuildId)
        .Property(_ => _.DiscordRoleId)
        .Property(_ => _.Id, typeof(UserIdToFullNameEntryValueConverter));
    }
  }
}