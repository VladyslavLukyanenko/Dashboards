namespace ProjectIndustries.Dashboards.Core.Identity.Services
{
  public interface IUserMessagesFactory
  {
    string CreateEmailConfirmMessage(string code);
    string CreatePhoneNumberChangeMessage(string code);
  }
}