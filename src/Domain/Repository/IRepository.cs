using System.Collections.Generic;

namespace Domain.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        
        T GetById(long id);
        
        void Save(T aggregateRoot);
        
    }
}
