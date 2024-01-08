namespace ProjectIndustries.Dashboards.Core.Events
{
  public class IdentityUnlocked : DomainEvent
  {
    public IdentityUnlocked(long id)
    {
      Id = id;
    }

    public long Id { get; }
  }
}