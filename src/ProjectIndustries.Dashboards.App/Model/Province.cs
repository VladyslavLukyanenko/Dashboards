#pragma warning disable 8618
namespace ProjectIndustries.Dashboards.App.Model
{
  public class Province
  {
    public string Code { get; set; }
    public string Title { get; set; }

    public override string ToString()
    {
      return $"{nameof(Country)}({Title}, {Code})";
    }
  }
}