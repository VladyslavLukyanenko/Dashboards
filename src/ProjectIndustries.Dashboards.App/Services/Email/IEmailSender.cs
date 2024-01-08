using System.Threading;
using System.Threading.Tasks;

namespace ProjectIndustries.Dashboards.App.Services.Email
{
  public interface IEmailSender
  {
    Task SendAsync(EmailMessage message, CancellationToken ct = default);
  }
}