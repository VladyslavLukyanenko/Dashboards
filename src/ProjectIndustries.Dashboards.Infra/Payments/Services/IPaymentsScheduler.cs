using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.Security;

namespace ProjectIndustries.Dashboards.Infra.Payments.Services
{
  public interface IPaymentsScheduler
  {
    ValueTask<Result> ScheduleSalaryPayoutAsync(Dashboard dashboard, UserMemberRoleBinding binding,
      CancellationToken ct = default);

    ValueTask<Result> CancelSalaryPayoutAsync(Dashboard dashboard, UserMemberRoleBinding binding,
      CancellationToken ct = default);

    ValueTask<Result> ScheduleSalaryPayoutAsync(Dashboard dashboard, IEnumerable<UserMemberRoleBinding> bindings,
      CancellationToken ct = default);
  }
}