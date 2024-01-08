using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectIndustries.Dashboards.Core.Embeds;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Infra.EfMappings;

namespace ProjectIndustries.Dashboards.Infra.Embeds.EfMappings
{
  public class EfDiscordEmbedWebHookBindingMapping : EntityMappingConfig<DiscordEmbedWebHookBinding>
  {
    protected override string SchemaName => "Embeds";

    public override void Configure(EntityTypeBuilder<DiscordEmbedWebHookBinding> builder)
    {
      builder.Property(_ => _.MessageTemplate)
        .HasConversion(t => ToJson(t), json => FromJson<EmbedMessageTemplate>(json)!);

      builder.HasOne<Dashboard>()
        .WithMany()
        .HasForeignKey(_ => _.DashboardId);

      builder.Property(_ => _.EventType).IsRequired();
      builder.Property(_ => _.WebhookUrl).IsRequired();
      
      base.Configure(builder);
    }
  }
}