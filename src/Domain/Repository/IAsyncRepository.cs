using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IAsyncRepository<T>
    {
        Task SaveAsync(T aggregateRoot);
        Task UpdateAsync(T aggregateRoot);
    }
}
