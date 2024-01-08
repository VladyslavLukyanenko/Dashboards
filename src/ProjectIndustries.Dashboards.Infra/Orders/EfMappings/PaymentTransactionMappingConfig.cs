using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectIndustries.Dashboards.Core.Identity;
using ProjectIndustries.Dashboards.Core.Orders;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Infra.EfMappings;

namespace ProjectIndustries.Dashboards.Infra.Orders.EfMappings
{
  public class PaymentTransactionMappingConfig : EntityMappingConfig<PaymentTransaction>
  {
    protected override string SchemaName => "Orders";

    public override void Configure(EntityTypeBuilder<PaymentTransaction> builder)
    {
      builder.HasOne<User>()
        .WithMany()
        .HasForeignKey(_ => _.UserId);

      builder.HasOne<Dashboard>()
        .WithMany()
        .HasForeignKey(_ => _.DashboardId);

      base.Configure(builder);
    }

    protected override void SetupIdGenerationStrategy(EntityTypeBuilder<PaymentTransaction> builder)
    {
      // no need HiLo here
    }
  }
}