using System;
using System.Collections.Generic;
using System.Reflection;
using Domain.Event;

namespace Application.Providers
{
    /// <summary>
    /// The <see cref="IDomainEventHandlersProvider"/> definition.
    /// </summary>
    public interface IDomainEventHandlersProvider
    {
        /// <summary>
        /// Initialize handlers from given assemblies.
        /// </summary>
        /// <param name="assemblies">Assemblies to scan.</param>
        void Init(IEnumerable<Assembly> assemblies);

        /// <summary>
        /// Returns an enumeration of <see cref="Type"/> that are able to handle an event of the given input <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="T">The input <see cref="Type"/>.</typeparam>
        /// <returns>An enumeration of <see cref="Type"/> that are able to handle an event of the given input <see cref="Type"/>.</returns>
        IEnumerable<Type> GetHandlersTypes<T>() where T : IDomainEvent;
    }
}
