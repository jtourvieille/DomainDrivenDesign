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

        protected abstract ParametrizedSqlQuery SaveCommand(T aggregateRoot);

        protected abstract ParametrizedSqlQuery UpdateCommand(T aggregateRoot);

        public async Task SaveAsync(T aggregateRoot)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                var command = SaveCommand(aggregateRoot);
                await transaction.Connection.ExecuteAsync(command.Sql, (object)command.Parameter);
                transaction.Commit();
            }
        }

        public async Task UpdateAsync(T aggregateRoot)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                var command = UpdateCommand(aggregateRoot);
                await transaction.Connection.ExecuteAsync(command.Sql, (object)command.Parameter);
                transaction.Commit();
            }
        }
    }
}
