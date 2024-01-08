using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Infra.EfMappings;

namespace ProjectIndustries.Dashboards.Infra.Products.EfMappingConfigs
{
  public class EfProductMappingConfig : EfDashboardBoundTypeConfigBase<Product>
  {
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
      builder.Property(_ => _.Features)
        .HasConversion(f => ToJson(f), json => FromJson<IList<ProductFeature>>(json)!);

      builder.Property(_ => _.Images)
        .HasConversion(f => ToJson(f), json => FromJson<IList<string>>(json)!);

      builder.Property(_ => _.Version).HasConversion(v => v.ToString(), s => Version.Parse(s));
      builder.OwnsOne(_ => _.Slug);

      base.Configure(builder);
    }
  }
}