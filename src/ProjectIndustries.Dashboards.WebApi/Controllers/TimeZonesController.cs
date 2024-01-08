using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectIndustries.Dashboards.WebApi.Foundation.Model;
using ControllerBase = ProjectIndustries.Dashboards.WebApi.Foundation.Mvc.Controllers.ControllerBase;

namespace ProjectIndustries.Dashboards.WebApi.Controllers
{
  public class TimeZonesController : ControllerBase
  {
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<TimeZoneData[]>))]
    public IActionResult GetSupportedTimeZones()
    {
      var timeZones = TimeZoneInfo.GetSystemTimeZones()
        .OrderBy(_ => _.BaseUtcOffset)
        .ThenBy(_ => _.DisplayName)
        .Select(_ => new TimeZoneData
        {
          Id = _.Id,
          Name = _.DisplayName
        });
      return Ok(timeZones);
    }
  }

  public class TimeZoneData
  {
    public string Id { get; set; }
    public string Name { get; set; }
  }
}