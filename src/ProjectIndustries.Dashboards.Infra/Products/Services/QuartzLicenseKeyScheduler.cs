using System.Threading;
using System.Threading.Tasks;
using ProjectIndustries.Dashboards.App.Products.Services;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.Products.Services;
using ProjectIndustries.Dashboards.Infra.Products.Services.Jobs;
using Quartz;

namespace ProjectIndustries.Dashboards.Infra.Products.Services
{
  public class QuartzLicenseKeyScheduler : ILicenseKeyScheduler
  {
    private const string LicenseKeyGroup = "LicenseKeys";
    private readonly ISchedulerFactory _schedulerFactory;

    public QuartzLicenseKeyScheduler(ISchedulerFactory schedulerFactory)
    {
      _schedulerFactory = schedulerFactory;
    }

    public async ValueTask ScheduleKeyRemovalAsync(LicenseKey key, CancellationToken ct = default)
    {
      ITrigger trigger = TriggerBuilder.Create()
        .WithIdentity(key.Id.ToString(), LicenseKeyGroup)
        .StartAt(key.TrialEndsAt!.Value.ToDateTimeOffset())
        .Build();

      IJobDetail detail = JobBuilder.Create<RemoveLicenseKeyJob>()
        .WithIdentity(key.Id.ToString(), LicenseKeyGroup)
        .UsingJobData(nameof(RemoveLicenseKeyJob.LicenseKeyId), key.Id)
        .UsingJobData(nameof(RemoveLicenseKeyJob.LicenseKeyValue), key.Value)
        .Build();

      var scheduler = await _schedulerFactory.GetScheduler(ct);
      await scheduler.ScheduleJob(detail, trigger, ct);
    }
  }
}