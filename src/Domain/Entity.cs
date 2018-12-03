namespace Domain
{
    /// <summary>
    /// Entity definition.
    /// </summary>
    public abstract class Entity<T>
        where T : struct
    {
        /// <summary>
        /// The unique identifier of the entity.
        /// </summary>
        public T Id { get; set; } = default(T);

        protected bool Equals(Entity<T> other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Entity<T>)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity<T> left, Entity<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity<T> left, Entity<T> right)
        {
            return !Equals(left, right);
        }
    }
}
