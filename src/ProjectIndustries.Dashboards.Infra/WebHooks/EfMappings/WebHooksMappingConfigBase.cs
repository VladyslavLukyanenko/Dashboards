using ProjectIndustries.Dashboards.Infra.EfMappings;

namespace ProjectIndustries.Dashboards.Infra.WebHooks.EfMappings
{
  public abstract class WebHooksMappingConfigBase<T> : EntityMappingConfig<T> where T : class
  {
    protected override string SchemaName => "WebHooks";
  }
}