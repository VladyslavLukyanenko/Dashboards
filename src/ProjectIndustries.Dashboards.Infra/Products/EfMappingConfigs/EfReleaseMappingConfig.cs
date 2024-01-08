﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectIndustries.Dashboards.Core.Products;

namespace ProjectIndustries.Dashboards.Infra.Products.EfMappingConfigs
{
  public class EfReleaseMappingConfig : EfProductsMappingConfigBase<Release>
  {
    public override void Configure(EntityTypeBuilder<Release> builder)
    {
      builder.HasOne<Plan>()
        .WithMany()
        .HasForeignKey(_ => _.PlanId);

      builder.Property(_ => _.Password).IsRequired();
      builder.Property(_ => _.Stock).UsePropertyAccessMode(PropertyAccessMode.Field);
      
      base.Configure(builder);
    }
  }
}