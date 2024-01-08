using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectIndustries.Dashboards.Core.Security;
using ProjectIndustries.Dashboards.Infra.EfMappings;

namespace ProjectIndustries.Dashboards.Infra.Security.EfMappingConfigs
{
  public class MemberRoleMappingConfig : SecurityMappingConfigBase<MemberRole>
  {
    public override void Configure(EntityTypeBuilder<MemberRole> builder)
    {
      builder.Property(_ => _.Permissions).UsePropertyAccessMode(PropertyAccessMode.Field)
        .HasConversion(p => ToJson(p), json => FromJson<HashSet<string>>(json)!);

      builder.Property(_ => _.Name).IsRequired();
      builder.HasIndex(_ => new {_.Name, _.DashboardId})
        .IsUnique();

      builder.Property(_ => _.Currency).HasNullableEnumerationConversion().IsRequired(false);
      builder.Property(_ => _.PayoutFrequency).HasNullableEnumerationConversion().IsRequired(false);

      base.Configure(builder);
    }
  }
}