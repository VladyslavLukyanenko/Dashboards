using ProjectIndustries.Dashboards.Core.Audit.Mappings;
using ProjectIndustries.Dashboards.Core.Identity;
using ProjectIndustries.Dashboards.Infra.Audit.EntryValueConverters;

namespace ProjectIndustries.Dashboards.Infra.Identity.AuditMappings
{
  public class UserAuditMapping : AuditMappingBase
  {
    public UserAuditMapping()
    {
      Map<User, long>()
        .Property(_ => _.Name)
        .Property(_ => _.UserName)
        .Property(_ => _.Discriminator)
        .Property(_ => _.DiscordId)
        .Property(_ => _.Avatar)
        .Property(_ => _.LastRefreshedAt)
        .Property(_ => _.StripeCustomerId)
        .Property(_ => _.Email.Value)
        .Property(_ => _.Email.IsConfirmed)
        .Property(_ => _.LockoutEnd)
        .Property(_ => _.Id, typeof(UserIdToRolesEntryValueConverter))
        .Property(_ => _.IsLockedOut);
    }
  }
}