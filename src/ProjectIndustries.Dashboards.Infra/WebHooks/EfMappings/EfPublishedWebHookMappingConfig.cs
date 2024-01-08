using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.WebHooks;

namespace ProjectIndustries.Dashboards.Infra.WebHooks.EfMappings
{
  public class EfPublishedWebHookMappingConfig : WebHooksMappingConfigBase<PublishedWebHook>
  {
    public override void Configure(EntityTypeBuilder<PublishedWebHook> builder)
    {
      builder.HasOne<Dashboard>()
        .WithMany()
        .HasForeignKey(_ => _.DashboardId);

      builder.OwnsOne(_ => _.Payload);
      base.Configure(builder);
    }
  }
}