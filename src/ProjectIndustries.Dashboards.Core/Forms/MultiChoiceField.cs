﻿using System.Collections.Generic;
using System.Linq;

namespace ProjectIndustries.Dashboards.Core.Forms
{
  public class MultiChoiceField : FormField
  {
    public IList<RichFormOptionValue> Options { get; set; } = new List<RichFormOptionValue>();

    protected override bool IsValueValid(FormFieldValue fieldValue) =>
      Options.Any(r => r.Id == fieldValue.Value);
  }
}