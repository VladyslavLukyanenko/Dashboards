using ProjectIndustries.Dashboards.Core.Audit.Mappings;

namespace ProjectIndustries.Dashboards.Core.Audit.Services
{
  public interface IEntityToChangeSetEntryMapperFactory
  {
    IEntityToChangeSetEntryMapper Create(IEntityMappingBuilder builder);
  }
}