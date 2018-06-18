namespace Domain.Repository
{
    public interface IRepository<T>
    {
        void Save(T aggregateRoot);
        void Update(T aggregateRoot);
    }
}
