using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectIndustries.Dashboards.Core.Identity;
using ProjectIndustries.Dashboards.Core.Security;

namespace ProjectIndustries.Dashboards.Infra.Security.EfMappingConfigs
{
  public class UserMemberRolesMappingConfig : SecurityMappingConfigBase<UserMemberRoleBinding>
  {
    public override void Configure(EntityTypeBuilder<UserMemberRoleBinding> builder)
    {
      builder.HasOne<User>()
        .WithMany()
        .HasForeignKey(_ => _.UserId);

      builder.HasOne<MemberRole>()
        .WithMany()
        .HasForeignKey(_ => _.MemberRoleId);
      
      base.Configure(builder);
    }
  }
}