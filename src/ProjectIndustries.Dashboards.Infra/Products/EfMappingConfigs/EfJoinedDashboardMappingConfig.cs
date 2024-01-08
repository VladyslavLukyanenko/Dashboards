using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectIndustries.Dashboards.Core.Identity;
using ProjectIndustries.Dashboards.Core.Products;

namespace ProjectIndustries.Dashboards.Infra.Products.EfMappingConfigs
{
  public class EfJoinedDashboardMappingConfig : EfProductsMappingConfigBase<JoinedDashboard>
  {
    public override void Configure(EntityTypeBuilder<JoinedDashboard> builder)
    {
      builder.HasKey(_ => new {_.DashboardId, _.UserId});
      builder.HasOne<Dashboard>()
        .WithMany()
        .HasForeignKey(_ => _.DashboardId);
      builder.HasOne<User>()
        .WithMany()
        .HasForeignKey(_ => _.UserId);

      base.Configure(builder);
    }
  }
}