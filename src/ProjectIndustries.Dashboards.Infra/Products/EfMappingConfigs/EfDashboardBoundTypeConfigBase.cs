using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectIndustries.Dashboards.Core;
using ProjectIndustries.Dashboards.Core.Products;

namespace ProjectIndustries.Dashboards.Infra.Products.EfMappingConfigs
{
  public class EfDashboardBoundTypeConfigBase<T> : EfProductsMappingConfigBase<T>
    where T: class, IDashboardBoundEntity
  {
    public override void Configure(EntityTypeBuilder<T> builder)
    {
      builder.HasOne<Dashboard>()
        .WithMany()
        .HasForeignKey(_ => _.DashboardId);

      base.Configure(builder);
    }
  }
}