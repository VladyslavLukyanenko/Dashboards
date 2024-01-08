﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using ProjectIndustries.Dashboards.Core.Security;
using ProjectIndustries.Dashboards.Core.Security.Services;

namespace ProjectIndustries.Dashboards.WebApi.Authorization
{
  public class HttpContextPermissionsAccessor : IPermissionsAccessor
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPermissionsRegistry _permissionsRegistry;

    public HttpContextPermissionsAccessor(IHttpContextAccessor httpContextAccessor,
      IPermissionsRegistry permissionsRegistry)
    {
      _httpContextAccessor = httpContextAccessor;
      _permissionsRegistry = permissionsRegistry;
    }

    public IReadOnlyList<string> CurrentRequiredPermissions
    {
      get
      {
        var values = _httpContextAccessor.HttpContext?.Request.RouteValues
                     ?? throw new InvalidOperationException("Requires http context");

        var controller = values["controller"]?.ToString();
        var action = values["action"]?.ToString();
        if (string.IsNullOrEmpty(controller) || string.IsNullOrEmpty(action))
        {
          throw new InvalidOperationException("Supported controller based endpoints only");
        }

        return _permissionsRegistry.GetPermissions(controller, action);
      }
    }
  }
}