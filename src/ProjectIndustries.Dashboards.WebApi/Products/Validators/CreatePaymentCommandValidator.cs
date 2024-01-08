using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Http;
using ProjectIndustries.Dashboards.App.Captcha;
using ProjectIndustries.Dashboards.Core.Products;
using ProjectIndustries.Dashboards.Core.Products.Services;
using ProjectIndustries.Dashboards.WebApi.Foundation.Authorization;
using ProjectIndustries.Dashboards.WebApi.Products.Commands;

namespace ProjectIndustries.Dashboards.WebApi.Products.Validators
{
  public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPlanRepository _planRepository;
    private readonly ICaptchaService _captchaService;

    public CreatePaymentCommandValidator(IHttpContextAccessor httpContextAccessor, IPlanRepository planRepository,
      ICaptchaService captchaService)
    {
      _httpContextAccessor = httpContextAccessor;
      _planRepository = planRepository;
      _captchaService = captchaService;
      RuleFor(_ => _).CustomAsync(CaptchaValueMustBeValid);
    }

    private async Task CaptchaValueMustBeValid(CreatePaymentCommand cmd, CustomContext context,
      CancellationToken ct)
    {
      var dashboardId = _httpContextAccessor.HttpContext!.User.GetDashboardId().GetValueOrDefault();
      Plan? plan = await _planRepository.GetByPasswordAsync(dashboardId, cmd.Password, ct);
      if (plan == null || !plan.ProtectPurchasesWithCaptcha)
      {
        return;
      }
      
      var captcha = cmd.CaptchaResponse;
      var result = await _captchaService.ValidateAsync(captcha);
      if (result.IsFailure)
      {
        var failure = new ValidationFailure(context.PropertyName, result.Error, context.PropertyValue)
        {
          ErrorCode = result.Error,
        };

        context.AddFailure(failure);
      }
    }
  }
}