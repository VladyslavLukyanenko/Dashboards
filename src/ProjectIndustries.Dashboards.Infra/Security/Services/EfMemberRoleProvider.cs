using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProjectIndustries.Dashboards.App.Security.Model;
using ProjectIndustries.Dashboards.App.Security.Services;
using ProjectIndustries.Dashboards.Core.Security;
using ProjectIndustries.Dashboards.Infra.Services;

namespace ProjectIndustries.Dashboards.Infra.Security.Services
{
  public class EfMemberRoleProvider : DataProvider, IMemberRoleProvider
  {
    private readonly IQueryable<MemberRole> _memberRoles;
    private readonly IQueryable<UserMemberRoleBinding> _userMemberRoleBindings;
    private readonly IMapper _mapper;

    public EfMemberRoleProvider(DbContext context, IMapper mapper) : base(context)
    {
      _mapper = mapper;
      _memberRoles = GetAliveDataSource<MemberRole>();
      _userMemberRoleBindings = GetDataSource<UserMemberRoleBinding>();
    }

    public async ValueTask<IList<MemberRoleData>> GetMemberRolesAsync(Guid dashboardId, CancellationToken ct = default)
    {
      return await _memberRoles.Where(_ => _.DashboardId == dashboardId)
        .ProjectTo<MemberRoleData>(_mapper.ConfigurationProvider)
        .OrderBy(_ => _.Name)
        .ToListAsync(ct);
    }

    public async ValueTask<IList<BoundMemberRoleData>> GetRolesAsync(Guid dashboardId, long userId,
      CancellationToken ct = default)
    {
      var query = from role in _memberRoles
        join binding in _userMemberRoleBindings on role.Id equals binding.MemberRoleId
        where binding.DashboardId == dashboardId && binding.UserId == userId
        select new BoundMemberRoleData
        {
          Permissions = role.Permissions,
          RoleName = role.Name,
          MemberRoleId = role.Id,
          ColorHex = role.ColorHex,
          RoleBindingId = binding.Id
        };

      return await query.OrderBy(_ => _.RoleName).ToListAsync(ct);
    }
  }
}