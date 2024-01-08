using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectIndustries.Dashboards.Core.Identity;
using ProjectIndustries.Dashboards.Core.Products;

namespace ProjectIndustries.Dashboards.Infra.Products.EfMappingConfigs
{
  public class EfDashboardMappingConfig : EfProductsMappingConfigBase<Dashboard>
  {
    public override void Configure(EntityTypeBuilder<Dashboard> builder)
    {
      builder.HasOne<User>()
        .WithMany()
        .HasForeignKey(_ => _.OwnerId);

      builder.Property(_ => _.Name).IsRequired();
      builder.OwnsOne(_ => _.StripeConfig, sob =>
      {
        sob.Property(_ => _.ApiKey).IsRequired(false);
        sob.Property(_ => _.WebHookEndpointSecret).IsRequired(false);
      });
      builder.OwnsOne(_ => _.HostingConfig, hb =>
      {
        hb.HasIndex(_ => new {_.Mode, _.DomainName})
          .IsUnique();
      });
      builder.OwnsOne(_ => _.DiscordConfig, db => db.OwnsOne(_ => _.OAuthConfig));
      builder.Property(_ => _.ChargeBackersExportEnabled).UsePropertyAccessMode(PropertyAccessMode.Field);

      base.Configure(builder);
    }

    protected override void SetupIdGenerationStrategy(EntityTypeBuilder<Dashboard> builder)
    {
      // we don't want HiLo here
    }
  }
}