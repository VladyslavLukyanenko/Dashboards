﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectIndustries.Dashboards.Core.Forms;
using ProjectIndustries.Dashboards.Core.Identity;

namespace ProjectIndustries.Dashboards.Infra.Forms.EfMappings
{
  public class FormResponseMappingConfig : EfFormsMappingConfigBase<FormResponse>
  {
    public override void Configure(EntityTypeBuilder<FormResponse> builder)
    {
      builder.HasOne<Form>()
        .WithMany()
        .HasForeignKey(_ => _.FormId);

      builder.HasOne<User>()
        .WithMany()
        .HasForeignKey(_ => _.RespondedBy);

      builder.OwnsMany(_ => _.FieldValues, ob =>
      {
        const string foreignKey = "FormResponseId";
        ob.WithOwner().HasForeignKey(foreignKey);
        ob.HasKey(nameof(FormFieldValue.FieldId), foreignKey);
        ob.HasOne<FormField>()
          .WithMany()
          .HasForeignKey(_ => _.FieldId);
        ob.Property(_ => _.Value).IsRequired();
        MappedToTableWithDefaults(ob);
      });

      base.Configure(builder);
    }
  }
}