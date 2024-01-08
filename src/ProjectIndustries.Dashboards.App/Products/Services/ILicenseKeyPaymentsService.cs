using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ProjectIndustries.Dashboards.Core.Identity;
using ProjectIndustries.Dashboards.Core.Products;

namespace ProjectIndustries.Dashboards.App.Products.Services
{
  public interface ILicenseKeyPaymentsService
  {
    ValueTask<Result> ProcessPaymentAsync(Dashboard dashboard, long planId, Release release, string intent,
      string customer, User user,
      string discordToken, CancellationToken ct = default);

    ValueTask<Result> AcquireTrialKeyAsync(Dashboard dashboard, long planId, Release release, User user,
      string discordToken,
      CancellationToken ct = default);
  }
}