using Microsoft.AspNetCore.Mvc;

namespace ProjectIndustries.Dashboards.WebApi.Foundation.Mvc
{
  public class ApiV1HttpRouteAttribute : RouteAttribute
  {
    public const string Prefix = "v1/";

    public ApiV1HttpRouteAttribute(string template)
      : base(Prefix + template)
    {
    }
  }
}