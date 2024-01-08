using NodaTime.Text;

namespace ProjectIndustries.Dashboards.Core.Forms
{
  public class TimeField : FormField
  {
    protected override bool IsValueValid(FormFieldValue fieldValue) =>
      LocalTimePattern.GeneralIso.Parse(fieldValue.Value).Success;
  }
}