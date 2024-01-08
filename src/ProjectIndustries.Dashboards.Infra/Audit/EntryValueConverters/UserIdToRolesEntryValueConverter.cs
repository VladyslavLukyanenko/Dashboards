using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.Identity.Services;
using ProjectIndustries.Dashboards.Core.Services;

namespace ProjectIndustries.Dashboards.Infra.Audit.EntryValueConverters
{
  public class UserIdToRolesEntryValueConverter : Int64ToStringEntryValueConverterBase
  {
    private readonly IUserRepository _userRepository;

    public UserIdToRolesEntryValueConverter(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    protected override async Task<string?> ConvertAsync(long id, CancellationToken ct = default)
    {
      var roleNames = await _userRepository.GetRolesAsync(id, ct);
      return string.Join(", ", roleNames);
    }
  }
}