using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectIndustries.Dashboards.Core.Audit.Mappings
{
  public interface IEntityToChangeSetEntryMapper
  {
    Type SupportedClrType { get; }
    string SupportedType { get; }
    ChangeSetEntry Map(object entity, ChangeType changeType);

    Task<Dictionary<string, string?>> ResolveMappedPropsAsync(ChangeSetEntry entry,
      CancellationToken ct = default);
  }
}