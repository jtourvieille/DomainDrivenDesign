using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Application.Providers;
using Application.Tests.AnotherAssembly;
using Domain;
using Domain.Event;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.Tests.Providers
{
    [TestClass]
    public class DomainEventHandlersProviderTests
    {
        [TestMethod]
        public void Should_NotThrowError_When_InitialisingWithNoAssembly()
        {
            // Arrange
            var domainEventHandlersProvider = new DomainEventHandlersProvider();

            // Act & Assert (should not throw exception)
            domainEventHandlersProvider.Init(null);
        }

        [TestMethod]
        public void Should_FindOneHandler_When_InitialisingWithOneAssemblyContainingOneHandler()
        {
            // Arrange
            var domainEventHandlersProvider = new DomainEventHandlersProvider();

            // Act
            domainEventHandlersProvider.Init(new List<Assembly> { typeof(WhateverDomainEventHandler).Assembly });

            // Assert
            var handlersTypes = domainEventHandlersProvider.GetHandlersTypes<WhateverDomainEvent>();
            Assert.IsNotNull(handlersTypes);
            Assert.AreEqual(1, handlersTypes.Count());
            var handlerType = handlersTypes.ElementAt(0);
            Assert.AreEqual(typeof(WhateverDomainEventHandler).FullName, handlerType.FullName);
        }

        [TestMethod]
        public void Should_FindOneHandler_When_InitialisingWithMultipleAssembliesContainingOneHandler()
        {
            // Arrange
            var domainEventHandlersProvider = new DomainEventHandlersProvider();

            // Act
            domainEventHandlersProvider.Init(new List<Assembly> { typeof(AggregateRoot<>).Assembly, typeof(DummyDomainEventHandler).Assembly });

            // Assert
            var handlersTypes = domainEventHandlersProvider.GetHandlersTypes<DummyDomainEvent>();
            Assert.IsNotNull(handlersTypes);
            Assert.AreEqual(1, handlersTypes.Count());
            var handlerType = handlersTypes.ElementAt(0);
            Assert.AreEqual(typeof(DummyDomainEventHandler).FullName, handlerType.FullName);
        }

        [TestMethod]
        public void Should_FindTwoHandlers_When_InitialisingWithMultipleAssembliesContainingOneHandlerEach()
        {
            // Arrange
            var domainEventHandlersProvider = new DomainEventHandlersProvider();

            // Act
            domainEventHandlersProvider.Init(new List<Assembly> { typeof(WhateverDomainEventHandler).Assembly, typeof(DummyDomainEventHandler).Assembly });

            // Assert
            var whateverHandlersTypes = domainEventHandlersProvider.GetHandlersTypes<WhateverDomainEvent>();
            Assert.IsNotNull(whateverHandlersTypes);
            Assert.AreEqual(1, whateverHandlersTypes.Count());
            var whateverHandlerType = whateverHandlersTypes.ElementAt(0);
            Assert.AreEqual(typeof(WhateverDomainEventHandler).FullName, whateverHandlerType.FullName);

            var dummyHandlersTypes = domainEventHandlersProvider.GetHandlersTypes<DummyDomainEvent>();
            Assert.IsNotNull(dummyHandlersTypes);
            Assert.AreEqual(1, dummyHandlersTypes.Count());
            var dummyHandlerType = dummyHandlersTypes.ElementAt(0);
            Assert.AreEqual(typeof(DummyDomainEventHandler).FullName, dummyHandlerType.FullName);
        }

        [TestMethod]
        public void Should_ThrowException_When_TypeIsNull()
        {
            // Arrange
            var domainEventHandlersProvider = new DomainEventHandlersProvider();

            // Act
            domainEventHandlersProvider.Init(new List<Assembly> { typeof(WhateverDomainEventHandler).Assembly });

            // Assert
            try
            {
                domainEventHandlersProvider.GetHandlersTypes(null);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
            }
        }

        [TestMethod]
        public void Should_ThrowException_When_TypeIsNotIDomainEvent()
        {
            // Arrange
            var domainEventHandlersProvider = new DomainEventHandlersProvider();

            // Act
            domainEventHandlersProvider.Init(new List<Assembly> { typeof(WhateverDomainEventHandler).Assembly });

            // Assert
            try
            {
                domainEventHandlersProvider.GetHandlersTypes(typeof(string));
                Assert.Fail();
            }
            catch (ArgumentException)
            {
            }
        }

        [TestMethod]
        public void Should_NotThrowException_When_TypeIsIDomainEvent()
        {
            // Arrange
            var domainEventHandlersProvider = new DomainEventHandlersProvider();

            // Act
            domainEventHandlersProvider.Init(new List<Assembly> { typeof(WhateverDomainEventHandler).Assembly });

            // Assert
            var handlersTypes = domainEventHandlersProvider.GetHandlersTypes<WhateverDomainEvent>();
            Assert.IsNotNull(handlersTypes);
            Assert.AreEqual(1, handlersTypes.Count());
            var handlerType = handlersTypes.ElementAt(0);
            Assert.AreEqual(typeof(WhateverDomainEventHandler).FullName, handlerType.FullName);
        }
    }

    public class WhateverDomainEventHandler : IDomainEventHandler<WhateverDomainEvent>
    {
        public Task HandleAsync(WhateverDomainEvent domainEvent)
        {
            throw new System.NotImplementedException();
        }
    }

    public class WhateverDomainEvent : IDomainEvent
    {

    }
}
