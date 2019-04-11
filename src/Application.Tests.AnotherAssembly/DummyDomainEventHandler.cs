using System.Threading.Tasks;
using Domain.Event;

namespace Application.Tests.AnotherAssembly
{
    public class DummyDomainEvent : IDomainEvent
    {

    }

    public class DummyDomainEventHandler : IDomainEventHandler<DummyDomainEvent>
    {
        public Task HandleAsync(DummyDomainEvent domainEvent)
        {
            throw new System.NotImplementedException();
        }
    }
}
