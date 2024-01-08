using System.Diagnostics.CodeAnalysis;

namespace ProjectIndustries.Dashboards.WebApi.Foundation.Model
{
  public static class ApiContractExtensions
  {
    public static ApiContract<T> ToApiContract<T>([MaybeNull] this T self)
    {
      return self is ApiContract<T> c ? c : new(self);
    }

    public static ApiContract<string> ToApiError([MaybeNull] this string self)
    {
      return new(new ApiError(self));
    }
  }
}