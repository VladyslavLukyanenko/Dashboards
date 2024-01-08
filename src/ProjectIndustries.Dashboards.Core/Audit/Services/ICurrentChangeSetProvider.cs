namespace ProjectIndustries.Dashboards.Core.Audit.Services
{
  public interface ICurrentChangeSetProvider
  {
    ChangeSet? CurrentChangSet { get; }
  }
}