using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using ProjectIndustries.Dashboards.App.Services.Email;
using ProjectIndustries.Dashboards.Core.Events;
using ProjectIndustries.Dashboards.Core.Identity;
using ProjectIndustries.Dashboards.Core.Identity.Services;
using ProjectIndustries.Dashboards.Core.Services;

namespace ProjectIndustries.Dashboards.Infra.Identity.Consumers
{
  public class UserCreatedEmailConfirmationSenderHandler : IConsumer<UserWithEmailCreated>
  {
    private readonly IEmailMessageFactory _emailMessageFactory;
    private readonly IEmailSender _emailSender;
    private readonly UserManager<User> _userManager;
    private readonly IUserMessagesFactory _userMessagesFactory;

    public UserCreatedEmailConfirmationSenderHandler(UserManager<User> userManager,
      IUserMessagesFactory userMessagesFactory, IEmailSender emailSender, IEmailMessageFactory emailMessageFactory)
    {
      _userManager = userManager;
      _userMessagesFactory = userMessagesFactory;
      _emailSender = emailSender;
      _emailMessageFactory = emailMessageFactory;
    }

    public async Task Consume(ConsumeContext<UserWithEmailCreated> context)
    {
      var m = context.Message;
      if (m.User.Email.IsConfirmed)
      {
        return;
      }

      var token = await _userManager.GenerateEmailConfirmationTokenAsync(m.User);
      var message = _userMessagesFactory.CreateEmailConfirmMessage(token);

      var emailMessage = _emailMessageFactory.Create("Email confirmation", message, m.User.Email.Value);
      await _emailSender.SendAsync(emailMessage, context.CancellationToken);
    }
  }
}