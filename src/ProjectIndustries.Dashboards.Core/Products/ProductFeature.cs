using System.Collections.Generic;
using ProjectIndustries.Dashboards.Core.Primitives;

namespace ProjectIndustries.Dashboards.Core.Products
{
  public class ProductFeature : ValueObject
  {
    public ProductFeature(string icon, string title, string desc)
    {
      Icon = icon;
      Title = title;
      Desc = desc;
    }

    public string Icon { get; private set; }
    public string Title { get; private set; }
    public string Desc { get; private set; }

    protected override IEnumerable<object> GetAtomicValues()
    {
      yield return Icon;
      yield return Title;
      yield return Desc;
    }
  }
}