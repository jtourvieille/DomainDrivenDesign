using System.Threading.Tasks;

namespace Domain.Event
{
    public interface IDomainEventDispatcher
    {
        Task DispatchDomainEventsAsync(AggregateRoot aggregateRoot);
    }
}
