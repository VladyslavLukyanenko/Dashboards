using ProjectIndustries.Dashboards.Core.Identity;

namespace ProjectIndustries.Dashboards.Core.Events
{
  public class UserWithEmailCreated : DomainEvent
  {
    public UserWithEmailCreated(User user)
    {
      User = user;
    }

    public User User { get; }
  }
}