﻿using System.Threading;
using System.Threading.Tasks;

namespace ProjectIndustries.Dashboards.Core.Services
{
  public interface IJsonSerializer
  {
    ValueTask<string> SerializeAsync<T>(T value, CancellationToken ct = default);
    ValueTask<T> DeserializeAsync<T>(string raw, CancellationToken ct = default);
  }
}