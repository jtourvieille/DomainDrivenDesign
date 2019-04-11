using System.Threading.Tasks;

namespace Domain.Event
{
    public interface IDomainEventHandler<in T> where T : IDomainEvent
    {
        Task HandleAsync(T domainEvent);
    }
}
