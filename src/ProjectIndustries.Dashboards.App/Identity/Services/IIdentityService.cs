using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.App.Identity.Model;
using ProjectIndustries.Dashboards.Core.Identity;

namespace ProjectIndustries.Dashboards.App.Identity.Services
{
  public interface IIdentityService
  {
    // Task<long> RegisterAsync(RegisterWithEmailCommand cmd, CancellationToken ct = default);
    // Task<string> ConfirmEmailAsync(ConfirmEmailCommand cmd, CancellationToken ct = default);

    // Task<long> CreateConfirmedAsync(CreateWithConfirmedEmailCommand cmd, CancellationToken ct = default);
    Task UpdateAsync(User user, UserData data, CancellationToken ct = default);
  }
}