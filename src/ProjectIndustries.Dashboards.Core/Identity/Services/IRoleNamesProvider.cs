﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectIndustries.Dashboards.Core.Identity.Services
{
  public interface IRoleNamesProvider
  {
    Task<IList<string>> GetRoleNamesAsync(int userId, CancellationToken ct = default);
    Task<IDictionary<long, IList<string>>> GetRoleNamesAsync(IEnumerable<long> userId, CancellationToken ct = default);
  }
}