using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectIndustries.Dashboards.Core.Forms;

namespace ProjectIndustries.Dashboards.Infra.Forms.EfMappings
{
  public class FormComponentMappingConfig : FormComponentsMappingConfigBase<FormComponent>
  {
  }

  public class FormSectionMappingConfig : FormComponentsMappingConfigBase<FormSection>
  {
    public FormSectionMappingConfig()
    {
      MappedToSeparateTable = false;
    }
  }

  public class FormFieldMappingConfig : FormComponentsMappingConfigBase<FormField>
  {
    public FormFieldMappingConfig()
    {
      MappedToSeparateTable = false;
    }

    public override void Configure(EntityTypeBuilder<FormField> builder)
    {
      builder.HasOne<FormSection>()
        .WithMany()
        .HasForeignKey(_ => _.SectionId);

      base.Configure(builder);
    }
  }

  public class LinearScaleFieldMappingConfig : FormComponentsMappingConfigBase<LinearScaleField>
  {
    public LinearScaleFieldMappingConfig()
    {
      MappedToSeparateTable = false;
    }

    public override void Configure(EntityTypeBuilder<LinearScaleField> builder)
    {
      builder.Property(_ => _.MaxLabel).IsRequired();
      builder.Property(_ => _.MinLabel).IsRequired();
      base.Configure(builder);
    }
  }

  public class MultiChoiceFieldMappingConfig : FormComponentsMappingConfigBase<MultiChoiceField>
  {
    public MultiChoiceFieldMappingConfig()
    {
      MappedToSeparateTable = false;
    }

    public override void Configure(EntityTypeBuilder<MultiChoiceField> builder)
    {
      builder.Property(_ => _.Options)
        .HasConversion(o => ToJson(o), json => FromJson<IList<RichFormOptionValue>>(json)!);
      base.Configure(builder);
    }
  }

  public class MultiChoiceGridFieldMappingConfig : FormComponentsMappingConfigBase<MultiChoiceGridField>
  {
    public MultiChoiceGridFieldMappingConfig()
    {
      MappedToSeparateTable = false;
    }

    public override void Configure(EntityTypeBuilder<MultiChoiceGridField> builder)
    {
      builder.Property(_ => _.Rows)
        .HasConversion(o => ToJson(o), json => FromJson<IList<FormOptionValue>>(json)!);

      builder.Property(_ => _.Columns)
        .HasConversion(o => ToJson(o), json => FromJson<IList<FormOptionValue>>(json)!);

      base.Configure(builder);
    }
  }

  public class TextBlockFieldMappingConfig : FormComponentsMappingConfigBase<TextBlockField>
  {
    public TextBlockFieldMappingConfig()
    {
      MappedToSeparateTable = false;
    }
  }

  public class ParagraphFieldMappingConfig : FormComponentsMappingConfigBase<ParagraphField>
  {
    public ParagraphFieldMappingConfig()
    {
      MappedToSeparateTable = false;
    }
  }

  public class CheckBoxesFieldMappingConfig : FormComponentsMappingConfigBase<CheckBoxesField>
  {
    public CheckBoxesFieldMappingConfig()
    {
      MappedToSeparateTable = false;
    }

    public override void Configure(EntityTypeBuilder<CheckBoxesField> builder)
    {
      builder.Property(_ => _.Options)
        .HasConversion(o => ToJson(o), json => FromJson<IList<RichFormOptionValue>>(json)!);
      base.Configure(builder);
    }
  }

  public class CheckBoxesGridFieldMappingConfig : FormComponentsMappingConfigBase<CheckBoxesGridField>
  {
    public CheckBoxesGridFieldMappingConfig()
    {
      MappedToSeparateTable = false;
    }

    public override void Configure(EntityTypeBuilder<CheckBoxesGridField> builder)
    {
      builder.Property(_ => _.Rows)
        .HasConversion(o => ToJson(o), json => FromJson<IList<FormOptionValue>>(json)!);

      builder.Property(_ => _.Columns)
        .HasConversion(o => ToJson(o), json => FromJson<IList<FormOptionValue>>(json)!);

      base.Configure(builder);
    }
  }

  public class DropDownFieldMappingConfig : FormComponentsMappingConfigBase<DropDownField>
  {
    public DropDownFieldMappingConfig()
    {
      MappedToSeparateTable = false;
    }

    public override void Configure(EntityTypeBuilder<DropDownField> builder)
    {
      builder.Property(_ => _.Options)
        .HasConversion(o => ToJson(o), json => FromJson<IList<FormOptionValue>>(json)!);
      base.Configure(builder);
    }
  }

  public class TextBoxFieldMappingConfig : FormComponentsMappingConfigBase<TextBoxField>
  {
    public TextBoxFieldMappingConfig()
    {
      MappedToSeparateTable = false;
    }
  }

  public class TimeFieldMappingConfig : FormComponentsMappingConfigBase<TimeField>
  {
    public TimeFieldMappingConfig()
    {
      MappedToSeparateTable = false;
    }
  }
}