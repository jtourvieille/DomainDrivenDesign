using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T GetById(long id);
        Task<T> GetByIdAsync(long id);
        void Save(T aggregateRoot);
        Task SaveAsync(T aggregateRoot);
    }
}
