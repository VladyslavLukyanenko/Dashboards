using ProjectIndustries.Dashboards.Infra.EfMappings;

namespace ProjectIndustries.Dashboards.Infra.Identity.EfMappings
{
  public abstract class EfIdentityMappingConfigBase<T> : EntityMappingConfig<T>
    where T : class
  {
    protected override string SchemaName => "Identity";
  }
}