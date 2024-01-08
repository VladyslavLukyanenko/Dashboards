using NodaTime.Text;

namespace ProjectIndustries.Dashboards.Core.Forms
{
  public class DateField : FormField
  {
    protected override bool IsValueValid(FormFieldValue fieldValue) =>
      LocalDatePattern.Iso.Parse(fieldValue.Value).Success;
  }
}