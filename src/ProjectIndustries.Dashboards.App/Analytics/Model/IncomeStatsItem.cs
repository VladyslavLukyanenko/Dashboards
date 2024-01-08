﻿namespace ProjectIndustries.Dashboards.App.Analytics.Model
{
  public class IncomeStatsItem
  {
    public ValueDiff<decimal> Amount { get; set; } = null!;
    public int GroupUnit { get; set; }
  }
}