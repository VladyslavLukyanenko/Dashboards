using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectIndustries.Dashboards.Core.Primitives
{
  public static class EnumExtensions
  {
    public static IEnumerable<T> GetFlags<T>(this T e) where T: Enum
    {
      return Enum.GetValues(e.GetType()).Cast<T>().Where(v => e.HasFlag(v));
    }
  }
}