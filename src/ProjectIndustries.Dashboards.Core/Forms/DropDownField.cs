using System.Collections.Generic;
using System.Linq;

namespace ProjectIndustries.Dashboards.Core.Forms
{
  public class DropDownField : FormField
  {
    public IList<FormOptionValue> Options { get; set; } = new List<FormOptionValue>();

    protected override bool IsValueValid(FormFieldValue fieldValue) =>
      Options.Any(o => o.Id == fieldValue.Value);
  }
}