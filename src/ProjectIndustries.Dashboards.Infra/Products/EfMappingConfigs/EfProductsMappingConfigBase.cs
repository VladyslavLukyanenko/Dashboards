using ProjectIndustries.Dashboards.Infra.EfMappings;

namespace ProjectIndustries.Dashboards.Infra.Products.EfMappingConfigs
{
  public abstract class EfProductsMappingConfigBase<T> : EntityMappingConfig<T>
    where T : class
  {
    protected override string SchemaName => "Products";
  }
}