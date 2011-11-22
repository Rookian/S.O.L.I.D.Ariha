using System;
using Core.Domain.Bases;
using Infrastructure.DataAccess.Session;
using NHibernate;

namespace Infrastructure.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISessionBuilder _sessionBuilder;

        public UnitOfWork(ISessionBuilder sessionBuilder)
        {
            _sessionBuilder = sessionBuilder;
        }

        public UnitOfWork(): this(new SessionBuilder())
        {

        }

        public void Begin()
        {
            if (ThereIsATransactionInProgress())
            {
                GetTransaction().Dispose();
            }

            GetSession().BeginTransaction();
        }

        public void RollBack()
        {
            if (GetTransaction().IsActive)
            {
                GetTransaction().Rollback();
            }
        }

        public ISession GetSession()
        {
            return _sessionBuilder.GetSession();
        }

        public void Dispose()
        {
            GetSession().Dispose();
        }

        public void Commit()
        {
            ITransaction transaction = GetTransaction();
            if (!transaction.IsActive)
                throw new InvalidOperationException("Must call Start() on the unit of work before committing");

            transaction.Commit();
        }

        private ITransaction GetTransaction()
        {
            return GetSession().Transaction;
        }

        private bool ThereIsATransactionInProgress()
        {
            return GetTransaction().IsActive || GetTransaction().WasCommitted || GetTransaction().WasRolledBack;
        }
    }
}