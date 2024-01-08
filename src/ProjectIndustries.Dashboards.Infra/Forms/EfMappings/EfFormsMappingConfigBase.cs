using ProjectIndustries.Dashboards.Infra.EfMappings;

namespace ProjectIndustries.Dashboards.Infra.Forms.EfMappings
{
  public abstract class EfFormsMappingConfigBase<T> : EntityMappingConfig<T> where T : class
  {
    protected override string SchemaName => "Forms";
  }
}