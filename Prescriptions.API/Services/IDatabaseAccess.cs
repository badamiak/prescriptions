using System;
using System.Collections.Generic;
using NHibernate;

namespace Prescriptions.API.Services
{
    public interface IDatabaseAccess
    {
        void Dispose();
        void Flush();
        IList<T> GetAllEntitiesOfType<T>() where T : class;
        IList<T> GetEntitiesOfType<T>(Func<ICriteria, ICriteria> criteriaStrategy) where T : class;
        IDatabaseAccess InitDbConnection();
        IDatabaseAccess InitDbConnection(string host);
        void Persist<T>(T entity);
    }
}