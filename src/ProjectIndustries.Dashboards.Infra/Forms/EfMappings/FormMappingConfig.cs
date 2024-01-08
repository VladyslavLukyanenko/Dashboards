using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectIndustries.Dashboards.Core.Forms;

namespace ProjectIndustries.Dashboards.Infra.Forms.EfMappings
{
  public class FormMappingConfig : EfFormsMappingConfigBase<Form>
  {
    public override void Configure(EntityTypeBuilder<Form> builder)
    {
      builder.OwnsOne(_ => _.Settings);
      builder.OwnsOne(_ => _.Theme);

      base.Configure(builder);
    }
  }
}