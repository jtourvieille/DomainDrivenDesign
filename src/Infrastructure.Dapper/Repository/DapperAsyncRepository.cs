using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Domain.Repository;
using Infrastructure.UnitOfWork;

namespace Infrastructure.Dapper.Repository
{
    public abstract class DapperAsyncRepository<T> : IAsyncRepository<T>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DapperAsyncRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected abstract ParametrizedQuery GetAllCommand();
        protected abstract ParametrizedQuery GetByIdCommand(long id);
        protected abstract ParametrizedQuery SaveCommand(T aggregateRoot);

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                var command = GetAllCommand();
                return await transaction.Connection.QueryAsync<T>(command.Sql, (object)command.Parameter);
            }
        }

        public async Task<T> GetByIdAsync(long id)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                var command = GetByIdCommand(id);
                return await transaction.Connection.QuerySingleAsync<T>(command.Sql, (object)command.Parameter);
            }
        }

        public async Task SaveAsync(T aggregateRoot)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                var command = SaveCommand(aggregateRoot);
                await transaction.Connection.ExecuteAsync(command.Sql, (object)command.Parameter);
                transaction.Commit();
            }
        }
    }
}
