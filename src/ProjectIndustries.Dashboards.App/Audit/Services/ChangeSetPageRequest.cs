using NodaTime;
using ProjectIndustries.Dashboards.Core.Audit;
using ProjectIndustries.Dashboards.Core.Collections;

namespace ProjectIndustries.Dashboards.App.Audit.Services
{
  public class ChangeSetPageRequest : PageRequest
  {
    public long? FacilityId { get; set; }
    public long? UserId { get; set; }
    public ChangeType? ChangeType { get; set; } = null!;

    public Instant From { get; set; }
    public Instant To { get; set; }
  }
}