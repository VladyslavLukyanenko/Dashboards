﻿namespace ProjectIndustries.Dashboards.Core.Services
{
  public interface IUserAgentService
  {
    UserAgentDeviceType ResolveDeviceType(string userAgent);
  }
}