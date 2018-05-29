using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Domain.Repository;
using Infrastructure.UnitOfWork;

namespace Infrastructure.Dapper.Repository
{
    public abstract class DapperRepository<T> : IRepository<T>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DapperRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected abstract string GetAllSql();
        protected abstract string GetByIdSql(long id);
        protected abstract string SaveSql(T aggregateRoot);

        public IEnumerable<T> GetAll()
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                return transaction.Connection.Query<T>(GetAllSql());
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                return await transaction.Connection.QueryAsync<T>(GetAllSql());
            }
        }

        public T GetById(long id)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                return transaction.Connection.QuerySingle<T>(GetByIdSql(id));
            }
        }

        public async Task<T> GetByIdAsync(long id)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                return await transaction.Connection.QuerySingleAsync<T>(GetByIdSql(id));
            }
        }

        public void Save(T aggregateRoot)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                transaction.Connection.Execute(SaveSql(aggregateRoot));
                transaction.Commit();
            }
        }

        public async Task SaveAsync(T aggregateRoot)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                await transaction.Connection.ExecuteAsync(SaveSql(aggregateRoot));
                transaction.Commit();
            }
        }
    }
}
