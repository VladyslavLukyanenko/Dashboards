﻿using NodaTime;

namespace ProjectIndustries.Dashboards.App.Analytics.Services
{
  public class GeneralAnalyticsRequest
  {
    public Offset Offset { get; set; }
    public string Start { get; set; } = null!;

    public string Period { get; set; } = null!;
  }
}