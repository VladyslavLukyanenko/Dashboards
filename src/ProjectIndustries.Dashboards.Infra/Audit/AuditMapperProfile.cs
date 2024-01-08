using AutoMapper;
using ProjectIndustries.Dashboards.App.Audit.Data;
using ProjectIndustries.Dashboards.App.Identity.Model;
using ProjectIndustries.Dashboards.Core.Audit;

namespace ProjectIndustries.Dashboards.Infra.Audit
{
  public class AuditMapperProfile : Profile
  {
    public AuditMapperProfile()
    {
      CreateMap<ChangeSet, ChangeSetData>()
        .ForMember(_ => _.UpdatedBy, _ => _.MapFrom(o => new UserRef {Id = o.UpdatedBy}))
        //.ForMember(_ => _.Facility, _ => _.MapFrom(o => new FacilityRef {Id = o.FacilityId.GetValueOrDefault()}))
        ;

      CreateMap<ChangeSetEntry, ChangeSetEntryRefData>();
    }
  }
}