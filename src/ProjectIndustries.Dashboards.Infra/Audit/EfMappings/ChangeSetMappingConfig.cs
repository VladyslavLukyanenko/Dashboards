using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectIndustries.Dashboards.Core;
using ProjectIndustries.Dashboards.Core.Audit;
using ProjectIndustries.Dashboards.Core.Identity;
using ProjectIndustries.Dashboards.Infra.EfMappings;

namespace ProjectIndustries.Dashboards.Infra.Audit.EfMappings
{
  public class ChangeSetMappingConfig : EntityMappingConfig<ChangeSet>
  {
    protected override string SchemaName => "Audit";

    public override void Configure(EntityTypeBuilder<ChangeSet> builder)
    {
      /*builder.HasOne<Facility>()
        .WithMany()
        .HasForeignKey(_ => _.FacilityId)
        .IsRequired(false);*/

      builder.HasOne<User>()
        .WithMany()
        .HasForeignKey(_ => _.UpdatedBy);

      builder.OwnsMany(_ => _.Entries, ob =>
        {
          ob.HasKey(_ => _.Id);
          ob.Property(_ => _.Payload)
            .HasColumnType("jsonb")
            .HasConversion(d => ToJson(d), json => FromJson<Dictionary<string, string>>(json)!);

          MappedToTableWithDefaults(ob);
        })
        .UsePropertyAccessMode(PropertyAccessMode.Field);

      base.Configure(builder);
    }

    protected override void SetupIdGenerationStrategy(EntityTypeBuilder<ChangeSet> builder)
    {
    }
  }
}