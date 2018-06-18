using Dapper;
using Domain.Repository;
using Infrastructure.UnitOfWork;

namespace Infrastructure.Dapper.Repository
{
    public abstract class DapperRepository<T> : IRepository<T>
    {
        protected readonly IUnitOfWork UnitOfWork;

        public DapperRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected abstract ParametrizedSqlQuery SaveCommand(T aggregateRoot);

        protected abstract ParametrizedSqlQuery UpdateCommand(T aggregateRoot);

        public void Save(T aggregateRoot)
        {
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                var command = SaveCommand(aggregateRoot);
                transaction.Connection.Execute(command.Sql, (object)command.Parameter, transaction: transaction);
                transaction.Commit();
            }
        }

        public void Update(T aggregateRoot)
        {
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                var command = UpdateCommand(aggregateRoot);
                transaction.Connection.Execute(command.Sql, (object)command.Parameter, transaction: transaction);
                transaction.Commit();
            }
        }
    }
}
