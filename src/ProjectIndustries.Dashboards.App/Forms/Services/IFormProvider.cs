using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.App.Forms.Model;

namespace ProjectIndustries.Dashboards.App.Forms.Services
{
  public interface IFormProvider
  {
    ValueTask<FormData?> GetUserFormAsync(long formId, long userId, CancellationToken ct = default);
  }
}