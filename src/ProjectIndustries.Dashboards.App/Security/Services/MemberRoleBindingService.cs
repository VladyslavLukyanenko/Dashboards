﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ProjectIndustries.Dashboards.App.Security.Model;
using ProjectIndustries.Dashboards.Core.Identity.Services;
using ProjectIndustries.Dashboards.Core.Security;
using ProjectIndustries.Dashboards.Core.Security.Services;

namespace ProjectIndustries.Dashboards.App.Security.Services
{
  public class MemberRoleBindingService : IMemberRoleBindingService
  {
    private readonly IMemberRoleRepository _memberRoleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserMemberRoleBindingRepository _userMemberRoleBindingRepository;

    public MemberRoleBindingService(IMemberRoleRepository memberRoleRepository, IUserRepository userRepository,
      IUserMemberRoleBindingRepository userMemberRoleBindingRepository)
    {
      _memberRoleRepository = memberRoleRepository;
      _userRepository = userRepository;
      _userMemberRoleBindingRepository = userMemberRoleBindingRepository;
    }

    public async ValueTask<Result> AssignRolesAsync(Guid dashboardId, IEnumerable<MemberRoleAssignmentData> data,
      CancellationToken ct = default)
    {
      var assignments = data as MemberRoleAssignmentData[] ?? data.ToArray();
      if (assignments.Length == 0)
      {
        return Result.Failure("No assignments provided");
      }

      var users = await _userRepository.GetByIdsAsync(assignments.Select(_ => _.UserId), ct);
      var roles = await _memberRoleRepository.GetDashboardRolesByIdsAsync(dashboardId,
        assignments.Select(_ => _.MemberRoleId), ct);

      var bindings = new List<UserMemberRoleBinding>(assignments.Length);
      foreach (var assignment in assignments)
      {
        var user = users.FirstOrDefault(_ => _.Id == assignment.UserId);
        var role = roles.FirstOrDefault(_ => _.Id == assignment.MemberRoleId);
        if (user == null || role == null)
        {
          return Result.Failure(
            $"Invalid assignment provided. Role: {assignment.MemberRoleId}, User: {assignment.UserId}");
        }

        var binding = new UserMemberRoleBinding(role, user);
        bindings.Add(binding);
      }

      await _userMemberRoleBindingRepository.CreateAsync(bindings, ct);
      return Result.Success();
    }
  }
}