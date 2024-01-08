using System;
using System.Runtime.Serialization;

namespace ProjectIndustries.Dashboards.App
{
  public class AppException : InvalidOperationException
  {
    public AppException()
    {
    }

    public AppException(string? message)
      : base(message)
    {
    }

    public AppException(string? message, Exception? innerException)
      : base(message, innerException)
    {
    }

    protected AppException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}