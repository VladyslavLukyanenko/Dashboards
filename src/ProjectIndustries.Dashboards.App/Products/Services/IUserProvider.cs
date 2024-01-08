using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.App.Identity.Model;
using ProjectIndustries.Dashboards.Core.Identity;

namespace ProjectIndustries.Dashboards.App.Products.Services
{
    public interface IUserProvider
    {
        Task<UserData?> GetUserByIdAsync(long id, CancellationToken ct = default);
    }
}