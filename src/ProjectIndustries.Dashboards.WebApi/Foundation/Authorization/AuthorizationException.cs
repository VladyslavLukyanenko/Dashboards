using System;
using System.Runtime.Serialization;
using ProjectIndustries.Dashboards.App;

namespace ProjectIndustries.Dashboards.WebApi.Foundation.Authorization
{
  public class AuthorizationException : AppException
  {
    public AuthorizationException()
      : this("Operation is not permitted")
    {
    }

    public AuthorizationException(string? message)
      : base(message)
    {
    }

    public AuthorizationException(string? message, Exception? innerException)
      : base(message, innerException)
    {
    }

    protected AuthorizationException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}