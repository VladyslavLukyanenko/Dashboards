using System;

namespace ProjectIndustries.Dashboards.Core.Audit.Mappings
{
  public interface IEntityMappingBuilder
  {
    AuditEntityMapping Mapping { get; }
    Type MappedType { get; }
    Type? PreProcessorType { get; }
  }
}