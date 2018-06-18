using Domain.Event;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Domain.Tests
{
    [TestClass]
    public class AggregareRootTests
    {
        private class SomethingHappenedEvent : IDomainEvent
        {
            public SomethingHappenedEvent(Guid guid)
            {
                Guid = guid;
            }

            public Guid Guid { get; }
        }
        private class MyAggregate : AggregateRoot
        {
            public void DoSomething(Guid guid)
            {
                this.AddDomainEvent(new SomethingHappenedEvent(guid));
            }
        }

        [TestMethod]
        public void Should_StoreOneEvent_When_AnEventHappened()
        {
            // Arrange
            Guid guid = new Guid();
            var myAggregate = new MyAggregate();

            // Act
            myAggregate.DoSomething(guid);

            // Assert
            Assert.AreEqual(1, myAggregate.DomainEvents.Count);
            foreach(var oneDomainEvent in myAggregate.DomainEvents)
            {
                Assert.IsInstanceOfType(oneDomainEvent, typeof(SomethingHappenedEvent));
                var somethingHappenedEvent = (SomethingHappenedEvent)oneDomainEvent;
                Assert.AreEqual(guid, somethingHappenedEvent.Guid);
            }
        }

        [TestMethod]
        public void Should_RemoveEvents_When_EventsHaveBeenConsumed()
        {
            // Arrange
            Guid guid = new Guid();
            var myAggregate = new MyAggregate();

            // Act
            myAggregate.DoSomething(guid);
            myAggregate.ClearDomainEvents();

            // Assert
            Assert.AreEqual(0, myAggregate.DomainEvents.Count);
        }
    }
}
