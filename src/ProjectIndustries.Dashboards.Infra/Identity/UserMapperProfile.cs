using ProjectIndustries.Dashboards.App.Identity.Model;
using ProjectIndustries.Dashboards.Core.Identity;
using Profile = AutoMapper.Profile;

namespace ProjectIndustries.Dashboards.Infra.Identity
{
  public class UserMapperProfile : Profile
  {
    public UserMapperProfile()
    {
      CreateMap<User, UserData>()
        .ForMember(_ => _.Email, _ => _.MapFrom(o => o.Email.Value))
        .ForMember(_ => _.IsEmailConfirmed, _ => _.MapFrom(o => o.Email.IsConfirmed))
        .ForMember(_ => _.IsLockedOut, _ => _.MapFrom(o => o.IsLockedOut))
        .ForMember(_ => _.Roles, _ => _.Ignore());
    }
  }
}