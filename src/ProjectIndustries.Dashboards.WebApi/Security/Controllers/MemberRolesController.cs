using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectIndustries.Dashboards.App.Security.Model;
using ProjectIndustries.Dashboards.App.Security.Services;
using ProjectIndustries.Dashboards.Core.Security;
using ProjectIndustries.Dashboards.Core.Security.Services;
using ProjectIndustries.Dashboards.WebApi.Foundation.Model;
using ProjectIndustries.Dashboards.WebApi.Foundation.Mvc.Controllers;

namespace ProjectIndustries.Dashboards.WebApi.Security.Controllers
{
  public class MemberRolesController : SecuredDashboardBoundControllerBase
  {
    private readonly IMemberRoleService _memberRoleService;
    private readonly IMemberRoleRepository _memberRoleRepository;
    private readonly IMemberRoleProvider _memberRoleProvider;

    public MemberRolesController(IServiceProvider provider, IMemberRoleService memberRoleService,
      IMemberRoleRepository memberRoleRepository, IMemberRoleProvider memberRoleProvider)
      : base(provider)
    {
      _memberRoleService = memberRoleService;
      _memberRoleRepository = memberRoleRepository;
      _memberRoleProvider = memberRoleProvider;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<MemberRoleData[]>))]
    public async ValueTask<IActionResult> GetRolesAsync(CancellationToken ct)
    {
      var roles = await _memberRoleProvider.GetMemberRolesAsync(CurrentDashboardId, ct);
      return Ok(roles);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> CreateAsync([FromBody] MemberRoleData role, CancellationToken ct)
    {
      await _memberRoleService.CreateAsync(CurrentDashboardId, role, ct);
      return NoContent();
    }

    [HttpPost("{roleId:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> UpdateAsync(long roleId, [FromBody] MemberRoleData data, CancellationToken ct)
    {
      MemberRole? role = await _memberRoleRepository.GetByIdAsync(roleId, ct);
      if (role == null)
      {
        return NotFound();
      }

      await _memberRoleService.UpdateAsync(role, data, ct);
      return NoContent();
    }

    [HttpDelete("{roleId:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> RemoveRoleAsync(long roleId, CancellationToken ct)
    {
      MemberRole? role = await _memberRoleRepository.GetByIdAsync(roleId, ct);
      if (role == null)
      {
        return NotFound();
      }

      _memberRoleRepository.Remove(role);
      return NoContent();
    }
  }
}