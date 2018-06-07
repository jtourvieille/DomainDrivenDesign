using System.Collections.Generic;
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

        protected abstract ParametrizedQuery GetAllCommand();
        protected abstract ParametrizedQuery GetByIdCommand(long id);
        protected abstract ParametrizedQuery SaveCommand(T aggregateRoot);

        public IEnumerable<T> GetAll()
        {
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                var command = GetAllCommand();
                return transaction.Connection.Query<T>(command.Sql, (object)command.Parameter);
            }
        }

        public T GetById(long id)
        {
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                var command = GetByIdCommand(id);
                return transaction.Connection.QuerySingle<T>(command.Sql, (object)command.Parameter);
            }
        }

        public void Save(T aggregateRoot)
        {
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                var command = SaveCommand(aggregateRoot);
                transaction.Connection.Execute(command.Sql, (object)command.Parameter, transaction: transaction);
                transaction.Commit();
            }
        }
    }
}
