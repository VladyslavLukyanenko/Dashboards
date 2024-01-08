using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectIndustries.Dashboards.Core.Analytics;
using ProjectIndustries.Dashboards.Core.Identity;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Infra.EfMappings;

namespace ProjectIndustries.Dashboards.Infra.Analytics.EfMappings
{
  public class UserSessionsMappingConfig : EntityMappingConfig<UserSession>
  {
    protected override string SchemaName => "Analytics";
    public override void Configure(EntityTypeBuilder<UserSession> builder)
    {
      builder.HasOne<User>()
        .WithMany()
        .HasForeignKey(_ => _.UserId);

      builder.HasOne<Dashboard>()
        .WithMany()
        .HasForeignKey(_ => _.DashboardId);

      base.Configure(builder);
    }

    protected override void SetupIdGenerationStrategy(EntityTypeBuilder<UserSession> builder)
    {
      // no need HiLo here
    }
  }
}