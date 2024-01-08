using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ProjectIndustries.Dashboards.App.Security.Model;
using ProjectIndustries.Dashboards.Core.Primitives;
using ProjectIndustries.Dashboards.Core.Security;
using ProjectIndustries.Dashboards.Core.Security.Services;

namespace ProjectIndustries.Dashboards.App.Security.Services
{
  public class MemberRoleService : IMemberRoleService
  {
    private readonly IMemberRoleManager _memberRoleManager;
    private readonly IMemberRoleRepository _memberRoleRepository;

    public MemberRoleService(IMemberRoleManager memberRoleManager, IMemberRoleRepository memberRoleRepository)
    {
      _memberRoleManager = memberRoleManager;
      _memberRoleRepository = memberRoleRepository;
    }

    public async ValueTask<Result<MemberRole>> CreateAsync(Guid dashboardId, MemberRoleData data,
      CancellationToken ct = default)
    {
      return await _memberRoleManager.CreateAsync(dashboardId, data.Name, data.Permissions, data.Salary,
        data.PayoutFrequency.ToEnumeration<PayoutFrequency?>(), data.Currency.ToEnumeration<Currency?>(), data.ColorHex,
        ct);
    }

    public ValueTask UpdateAsync(MemberRole role, MemberRoleData data, CancellationToken ct = default)
    {
      role.ChangePermissions(data.Permissions);
      _memberRoleRepository.Update(role);
      return default;
    }
  }
}