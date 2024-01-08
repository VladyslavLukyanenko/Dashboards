using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectIndustries.Dashboards.Core.Events;
using ProjectIndustries.Dashboards.Core.Primitives;

namespace ProjectIndustries.Dashboards.Infra
{
  public static class MediatorExtensions
  {
    public static async Task DispatchDomainEvents(this IMessageDispatcher messageDispatcher, DbContext context)
    {
      var domainEntries = context.ChangeTracker
        .Entries<IEventSource>()
        .Where(_ => _.Entity.DomainEvents.Any())
        .ToList();

      var domainEvents = domainEntries.SelectMany(_ => _.Entity.DomainEvents)
        .ToList();

      domainEntries.ForEach(entry => entry.Entity.ClearDomainEvents());

      foreach (IDomainEvent domainEvent in domainEvents)
      {
        await messageDispatcher.PublishEventAsync(domainEvent);
      }
    }
  }
}