using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace ProjectIndustries.Dashboards.App.Captcha
{
  public interface ICaptchaService
  {
    ValueTask<Result> ValidateAsync(string captchaResponse);
  }
}