using System;

namespace ProjectIndustries.Dashboards.App.WebHooks
{
  [AttributeUsage(AttributeTargets.Class)]
  public class WebHookTypeAttribute : Attribute
  {
    public WebHookTypeAttribute(string name)
    {
      Name = name;
    }
    public string Name { get; }
  }
}