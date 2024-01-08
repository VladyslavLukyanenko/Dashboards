namespace ProjectIndustries.Dashboards.Core.Audit.Processors
{
  public class NullAuditEntityPreProcessor : IAuditingEntityPreProcessor
  {
    public object PreProcess(object entity)
    {
      return entity;
    }
  }
}