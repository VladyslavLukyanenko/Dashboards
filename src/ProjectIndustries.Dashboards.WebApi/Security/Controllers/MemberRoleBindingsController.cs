using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectIndustries.Dashboards.App.Products.Model;
using ProjectIndustries.Dashboards.App.Security.Model;
using ProjectIndustries.Dashboards.App.Security.Services;
using ProjectIndustries.Dashboards.Core.Collections;
using ProjectIndustries.Dashboards.Core.Security.Services;
using ProjectIndustries.Dashboards.WebApi.Foundation.Model;
using ProjectIndustries.Dashboards.WebApi.Foundation.Mvc.Controllers;

namespace ProjectIndustries.Dashboards.WebApi.Security.Controllers
{
  public class MemberRoleBindingsController : SecuredDashboardBoundControllerBase
  {
    private readonly IMemberRoleBindingProvider _memberRoleBindingProvider;
    private readonly IUserMemberRoleBindingRepository _userMemberRoleBindingRepository;
    private readonly IMemberRoleBindingService _memberRoleBindingService;

    public MemberRoleBindingsController(IServiceProvider provider, IMemberRoleBindingProvider memberRoleBindingProvider,
      IUserMemberRoleBindingRepository userMemberRoleBindingRepository,
      IMemberRoleBindingService memberRoleBindingService) : base(provider)
    {
      _memberRoleBindingProvider = memberRoleBindingProvider;
      _userMemberRoleBindingRepository = userMemberRoleBindingRepository;
      _memberRoleBindingService = memberRoleBindingService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<IPagedList<StaffMemberData>>))]
    public async ValueTask<IActionResult> GetStaffMembersPageAsync([FromQuery] StaffMemberPageRequest pageRequest,
      CancellationToken ct)
    {
      var page = await _memberRoleBindingProvider.GetMembersPageAsync(CurrentDashboardId, pageRequest, ct);
      return Ok(page);
    }

    [HttpGet("Roles")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<IList<StaffRoleMembersData>>))]
    public async ValueTask<IActionResult> GetRolesAsync(CancellationToken ct)
    {
      var list = await _memberRoleBindingProvider.GetRolesAsync(CurrentDashboardId, ct);
      return Ok(list);
    }

    [HttpGet("{userId:long}/Summary")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiContract<MemberSummaryData>))]
    public async ValueTask<IActionResult> GetSummaryAsync(long userId, CancellationToken ct)
    {
      var summary = await _memberRoleBindingProvider.GetSummaryAsync(userId, CurrentDashboardId, ct);
      if (summary == null)
      {
        return NotFound();
      }

      return Ok(summary);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> AssignRolesAsync([FromBody] IList<MemberRoleAssignmentData> assignments,
      CancellationToken ct)
    {
      var r = await _memberRoleBindingService.AssignRolesAsync(CurrentDashboardId, assignments, ct);
      if (r.IsFailure)
      {
        return BadRequest(r.Error);
      }

      return NoContent();
    }


    [HttpDelete("{userId:long}/Roles/{roleId:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async ValueTask<IActionResult> RemoveMemberAsync(long userId, long roleId, CancellationToken ct)
    {
      var binding = await _userMemberRoleBindingRepository.GetUserRoleBindingAsync(userId, roleId, ct);
      if (binding == null)
      {
        return NotFound();
      }

      _userMemberRoleBindingRepository.Remove(binding);
      return NoContent();
    }
  }
}