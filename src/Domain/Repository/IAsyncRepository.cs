using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IAsyncRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(long id);
        Task SaveAsync(T aggregateRoot);
    }
}
