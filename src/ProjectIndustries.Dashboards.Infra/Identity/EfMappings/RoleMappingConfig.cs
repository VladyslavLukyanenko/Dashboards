using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectIndustries.Dashboards.Core.Identity;

namespace ProjectIndustries.Dashboards.Infra.Identity.EfMappings
{
  public class RoleMappingConfig : EfIdentityMappingConfigBase<Role>
  {
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
      builder.HasIndex(r => r.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique();
      builder.Property(u => u.Name).HasMaxLength(256);
      builder.Property(u => u.NormalizedName).HasMaxLength(256);
      base.Configure(builder);
    }
  }
}