using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ProjectIndustries.Dashboards.Core.ChargeBackers;
using ProjectIndustries.Dashboards.Core.Products;

namespace ProjectIndustries.Dashboards.App.ChargeBackers.Services
{
  public interface IChargeBackerExportService
  {
    ValueTask<Result> ExportAsync(ChargeBacker chargeBacker, Dashboard dashboard, CancellationToken ct = default);
  }
}