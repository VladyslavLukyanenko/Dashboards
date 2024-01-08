using System;
using System.Collections.Generic;

namespace ProjectIndustries.Dashboards.Core.Audit.Mappings
{
  public interface IAuditMappingProvider
  {
    IDictionary<Type, IEntityMappingBuilder> Builders { get; }
  }
}