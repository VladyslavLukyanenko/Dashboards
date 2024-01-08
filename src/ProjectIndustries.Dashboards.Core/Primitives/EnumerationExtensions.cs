using System.Diagnostics.CodeAnalysis;

namespace ProjectIndustries.Dashboards.Core.Primitives
{
  public static class EnumerationExtensions
  {
    public static T ToEnumeration<T>(this int id)
      where T : Enumeration?
    {
      return Enumeration.FromValue<T>(id);
    }

    [return: MaybeNull]
    public static T ToEnumeration<T>(this int? id)
      where T : Enumeration?
    {
      if (!id.HasValue)
      {
        return null;
      }

      return Enumeration.FromValue<T>(id.Value);
    }
  }
}