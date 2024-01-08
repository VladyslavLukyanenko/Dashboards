namespace ProjectIndustries.Dashboards.Core.Audit.Processors
{
  public interface IAuditingEntityPreProcessor
  {
    object PreProcess(object entity);
  }
}