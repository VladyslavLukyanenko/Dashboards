﻿namespace ProjectIndustries.Dashboards.Core.Forms
{
  public class ParagraphField : FormField
  {
    public string? Placeholder { get; set; }
    protected override bool IsValueValid(FormFieldValue fieldValue) => true;
  }
}