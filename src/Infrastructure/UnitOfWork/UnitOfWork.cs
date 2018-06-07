using System;
using System.Data;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction = null;

        public UnitOfWork(IDbConnection connection)
        {
            _connection = connection;
        }

        public IDbTransaction BeginTransaction()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("Can not handle multiple transactions. A previous transaction has already began.");
            }

            _transaction = _connection.BeginTransaction();
            return _transaction;
        }

        public IDbConnection Connection { get { return _connection; } }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
            }
        }
    }
}
