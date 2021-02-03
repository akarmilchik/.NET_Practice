using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entityItem);

        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
    }
}
