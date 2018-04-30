using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    /// <summary>
    /// ValueObject definition.
    /// </summary>
    /// <typeparam name="T">The underlying ValueObject.</typeparam>
    public abstract class ValueObject<T>
        where T : ValueObject<T>
    {
        /// <summary>
        /// Properties taking part of object comparison.
        /// </summary>
        /// <returns>A list of involved properties.</returns>
        protected abstract IEnumerable<object> GetEqualityComponents();

        private bool EqualsCore(ValueObject<T> other)
        {
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return EqualsCore((ValueObject<T>)obj);
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(1, (current, obj) => current * 23 + (obj?.GetHashCode() ?? 0));
        }

        public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
        {
            return !Equals(left, right);
        }
    }
}
