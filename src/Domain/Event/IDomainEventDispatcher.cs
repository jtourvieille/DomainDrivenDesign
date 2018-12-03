using System.Threading.Tasks;

namespace Domain.Event
{
    public interface IDomainEventDispatcher
    {
        Task DispatchDomainEventsAsync<T>(AggregateRoot<T> aggregateRoot) where T : struct;
    }
}
