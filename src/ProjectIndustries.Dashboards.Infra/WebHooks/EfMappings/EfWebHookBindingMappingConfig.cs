﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.WebHooks;

namespace ProjectIndustries.Dashboards.Infra.WebHooks.EfMappings
{
  public class EfWebHookBindingMappingConfig : WebHooksMappingConfigBase<WebHookBinding>
  {
    public override void Configure(EntityTypeBuilder<WebHookBinding> builder)
    {
      builder.HasOne<Dashboard>()
        .WithMany()
        .HasForeignKey(_ => _.DashboardId);
      builder.Property(_ => _.EventType).IsRequired();
      builder.Property(_ => _.ListenerEndpoint).IsRequired();

      base.Configure(builder);
    }
  }
}