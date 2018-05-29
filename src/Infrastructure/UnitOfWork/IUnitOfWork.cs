using System;
using System.Data;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDbTransaction BeginTransaction();
    }
}
