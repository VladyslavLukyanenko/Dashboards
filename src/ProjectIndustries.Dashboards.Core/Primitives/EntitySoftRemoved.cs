using ProjectIndustries.Dashboards.Core.Events;

namespace ProjectIndustries.Dashboards.Core.Primitives
{
  public class EntitySoftRemoved : DomainEvent
  {
    public EntitySoftRemoved(ISoftRemovable source)
    {
      Source = source;
    }
    
    public ISoftRemovable Source { get; }
  }
}