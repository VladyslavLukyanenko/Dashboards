#pragma warning disable 8618
using ProjectIndustries.Dashboards.Core.Products;

namespace ProjectIndustries.Dashboards.Infra.Configs
{
  public class StripeGlobalConfig
  {
    public string DashboardUrl { get; set; }

    public string GetBillingDashboardReturnUrl(Dashboard dashboard) => "https://projectindustries.gg/dashboard";

    public string GetPaymentSuccessfulUrl(Dashboard dashboard) => "https://projectindustries.gg/payments/successful";

    public string GetPaymentCancelledUrl(Dashboard dashboard) => "https://projectindustries.gg/payments/cancelled";
  }
}