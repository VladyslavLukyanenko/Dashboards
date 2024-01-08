using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.Security;

namespace ProjectIndustries.Dashboards.Infra.Payments.Services
{
  public interface IPaymentsService
  {
    ValueTask<Result> PayoutSalaryDueAsync(Dashboard dashboard, UserMemberRoleBinding binding,
      CancellationToken ct = default);

    ValueTask<Result> PayoutSalaryDueAsync(Dashboard dashboard, IEnumerable<UserMemberRoleBinding> bindings,
      CancellationToken ct = default);
  }
}