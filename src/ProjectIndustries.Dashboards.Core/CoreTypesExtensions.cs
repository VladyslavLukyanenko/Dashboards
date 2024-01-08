using System;
using System.Linq;
using ProjectIndustries.Dashboards.Core.Primitives;

namespace ProjectIndustries.Dashboards.Core
{
  public static class CoreTypesExtensions
  {
    private static readonly Type EntityTypeDef = typeof(IEntity<>);
    
    public static bool IsEntity(this Type type)
    {
      return type.GetInterfaces()
        .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == EntityTypeDef);
    }
  }
}