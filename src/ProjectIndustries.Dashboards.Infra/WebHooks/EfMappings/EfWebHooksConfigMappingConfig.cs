using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.WebHooks;

namespace ProjectIndustries.Dashboards.Infra.WebHooks.EfMappings
{
  public class EfWebHooksConfigMappingConfig : WebHooksMappingConfigBase<WebHooksConfig>
  {
    public override void Configure(EntityTypeBuilder<WebHooksConfig> builder)
    {
      builder.HasOne<Dashboard>()
        .WithMany()
        .HasForeignKey(_ => _.DashboardId);

      base.Configure(builder);
    }
  }
}