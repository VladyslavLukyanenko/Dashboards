using System;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.Core.Products.Services;
using ProjectIndustries.Dashboards.Core.Security.Services;
using ProjectIndustries.Dashboards.Infra.Payments.Services;
using Quartz;

namespace ProjectIndustries.Dashboards.Infra.Payments.Jobs
{
  public class PayoutJob : IJob
  {
    private readonly IPaymentsService _paymentsService;
    private readonly IDashboardRepository _dashboardRepository;
    private readonly IUserMemberRoleBindingRepository _userMemberRoleBindingRepository;

    public PayoutJob(IPaymentsService paymentsService, IDashboardRepository dashboardRepository,
      IUserMemberRoleBindingRepository userMemberRoleBindingRepository)
    {
      _paymentsService = paymentsService;
      _dashboardRepository = dashboardRepository;
      _userMemberRoleBindingRepository = userMemberRoleBindingRepository;
    }

    public async Task Execute(IJobExecutionContext context)
    {
      try
      {
        var dashboard = await _dashboardRepository.GetByIdAsync(DashboardId, context.CancellationToken);
        var binding = await _userMemberRoleBindingRepository.GetByIdAsync(BindingId, context.CancellationToken);
        await _paymentsService.PayoutSalaryDueAsync(dashboard!, binding!, context.CancellationToken);
      }
      catch (Exception exc)
      {
        throw new JobExecutionException(exc);
      }
    }

    public Guid DashboardId { get; set; }
    public long BindingId { get; set; }
  }
}