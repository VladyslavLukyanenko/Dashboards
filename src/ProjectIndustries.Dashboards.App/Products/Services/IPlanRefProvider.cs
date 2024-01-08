using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.App.Products.Model;

namespace ProjectIndustries.Dashboards.App.Products.Services
{
  public interface IPlanRefProvider
  {
    ValueTask<PlanRef> GetRefAsync(long planId, CancellationToken ct = default);
  }
}