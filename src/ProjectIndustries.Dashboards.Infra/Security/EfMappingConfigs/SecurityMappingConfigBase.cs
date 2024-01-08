using ProjectIndustries.Dashboards.Infra.EfMappings;

namespace ProjectIndustries.Dashboards.Infra.Security.EfMappingConfigs
{
  public abstract class SecurityMappingConfigBase<T> : EntityMappingConfig<T>
    where T : class
  {
    protected override string SchemaName => "Security";
  }
}