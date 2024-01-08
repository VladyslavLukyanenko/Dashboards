using System;
using ProjectIndustries.Dashboards.Core.Audit.Mappings;

namespace ProjectIndustries.Dashboards.Core.Audit.Services
{
  public interface IChangeSetMapperProvider
  {
    IEntityToChangeSetEntryMapper GetMapper(Type entityType);
    IEntityToChangeSetEntryMapper GetMapper(string entityType);
    bool HasMapperForType(Type type);
    bool HasMapperForType(string entityType);
  }
}