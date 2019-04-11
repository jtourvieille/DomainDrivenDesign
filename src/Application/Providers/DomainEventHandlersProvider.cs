using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Domain.Event;

namespace Application.Providers
{
    /// <summary>
    /// The <see cref="IDomainEventHandlersProvider"/> definition.
    /// </summary>
    public class DomainEventHandlersProvider : IDomainEventHandlersProvider
    {
        private readonly IDictionary<Type, IList<Type>> handlersByHandledType = new Dictionary<Type, IList<Type>>();

        /// <summary>
        /// Initialize handlers from given assemblies.
        /// </summary>
        /// <param name="assemblies">Assemblies to scan.</param>
        public void Init(IEnumerable<Assembly> assemblies)
        {
            if (assemblies != null)
            {
                this.handlersByHandledType.Clear();

                foreach (var assembly in assemblies)
                {
                    this.ExploreAssembly(assembly);
                }
            }
        }

        /// <summary>
        /// Returns an enumeration of <see cref="Type"/> that are able to handle an event of the given input <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="T">The input <see cref="Type"/>.</typeparam>
        /// <returns>An enumeration of <see cref="Type"/> that are able to handle an event of the given input <see cref="Type"/>.</returns>
        public IEnumerable<Type> GetHandlersTypes<T>() where T : IDomainEvent
        {
            return this.GetHandlersTypes(typeof(T));
        }

        public IEnumerable<Type> GetHandlersTypes(Type domainEventType)
        {
            if (domainEventType == null)
            {
                throw new ArgumentNullException(nameof(domainEventType));
            }

            if (!domainEventType.GetInterfaces().Contains(typeof(IDomainEvent)))
            {
                throw new ArgumentException(nameof(domainEventType), $"The parameter must be of type {typeof(IDomainEvent).FullName}");
            }

            if (this.handlersByHandledType.ContainsKey(domainEventType))
            {
                return this.handlersByHandledType[domainEventType];
            }

            return null;
        }

        private void ExploreAssembly(Assembly assembly)
        {
            var handlers = assembly
                .GetTypes()
                .Where(type => type.GetInterfaces().Any(implementedInterface =>
                    implementedInterface.IsGenericType && implementedInterface.GetGenericTypeDefinition() ==
                    typeof(IDomainEventHandler<>)))
                .ToList();

            foreach (var handler in handlers)
            {
                var handledType = handler.GetInterfaces()
                    .Where(implementedInterface =>
                        implementedInterface.IsGenericType &&
                        implementedInterface.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>))
                    .Select(oneInterface => oneInterface.GenericTypeArguments[0])
                    .First();

                if (!this.handlersByHandledType.ContainsKey(handledType))
                {
                    this.handlersByHandledType.Add(handledType, new List<Type> { handler });
                }
                else
                {
                    this.handlersByHandledType[handledType].Add(handler);
                }
            }
        }
    }
}
