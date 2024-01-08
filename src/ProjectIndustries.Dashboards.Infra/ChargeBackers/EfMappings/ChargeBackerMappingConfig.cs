using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectIndustries.Dashboards.Core.ChargeBackers;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Infra.EfMappings;

namespace ProjectIndustries.Dashboards.Infra.ChargeBackers.EfMappings
{
  public class ChargeBackerMappingConfig : EntityMappingConfig<ChargeBacker>
  {
    protected override string SchemaName => "ChargeBackers";

    public override void Configure(EntityTypeBuilder<ChargeBacker> builder)
    {
      builder.HasOne<Dashboard>()
        .WithMany()
        .HasForeignKey(_ => _.DashboardId);

      builder.Property(_ => _.CardFingerprints)
        .UsePropertyAccessMode(PropertyAccessMode.Field)
        .HasConversion(cards => ToJson(cards), json => FromJson<IReadOnlyList<string>>(json)!);

      base.Configure(builder);
    }
  }
}