using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectIndustries.Dashboards.App.Model;
using ProjectIndustries.Dashboards.WebApi.Foundation.Mvc.Controllers;

namespace ProjectIndustries.Dashboards.WebApi.Controllers
{
  public class CountriesController : SecuredControllerBase
  {
    public CountriesController(IServiceProvider provider) : base(provider)
    {
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(List<Country>), StatusCodes.Status200OK)]
    public IActionResult GetListAsync()
    {
      return File("~/countries.json", "application/json");
    }
  }
}