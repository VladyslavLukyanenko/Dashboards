using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectIndustries.Dashboards.Core.Forms
{
  public class CheckBoxesField : FormField
  {
    public IList<RichFormOptionValue> Options { get; set; } = new List<RichFormOptionValue>();

    protected override bool IsValueValid(FormFieldValue fieldValue)
    {
      var selectedValues = fieldValue.Value.Split(';', StringSplitOptions.RemoveEmptyEntries);
      return selectedValues.Length < Options.Count
             && selectedValues.All(v => Options.Any(o => o.Id == v));
    }
  }
}