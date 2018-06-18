using Domain.Event;
using System.Collections.Generic;

namespace Domain
{
    /// <summary>
    /// AggregateRoot definition
    /// </summary>
    public abstract class AggregateRoot : Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

        protected virtual void AddDomainEvent(IDomainEvent newEvent)
        {
            if (newEvent == null)
            {
                return;
            }

            _domainEvents.Add(newEvent);
        }

        public virtual void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}