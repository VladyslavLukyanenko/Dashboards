#pragma warning disable 8618
namespace ProjectIndustries.Dashboards.Core.Products
{
  public class StripeSessionData
  {
    public long PlanId { get; set; }
    public string Password { get; set; }
    public string Customer { get; set; }
    public string Intent { get; set; }
  }
}