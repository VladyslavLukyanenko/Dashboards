using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.Audit;
using ProjectIndustries.Dashboards.Core.Audit.Services;

namespace ProjectIndustries.Dashboards.App.Audit.Services
{
  public class ChangeSetEntryPayloadMapper
    : IChangeSetEntryPayloadMapper
  {
    private readonly IChangeSetMapperProvider _mapperProvider;

    public ChangeSetEntryPayloadMapper(IChangeSetMapperProvider mapperProvider)
    {
      _mapperProvider = mapperProvider;
    }

    public async Task<IDictionary<string, string?>?> MapAsync(ChangeSetEntry entry, CancellationToken ct = default)
    {
      if (entry == null)
      {
        return null;
      }

      if (!_mapperProvider.HasMapperForType(entry.EntityType))
      {
        throw new InvalidOperationException("Can't map " + entry.EntityType);
      }

      return await _mapperProvider.GetMapper(entry.EntityType).ResolveMappedPropsAsync(entry, ct);
    }
  }
}