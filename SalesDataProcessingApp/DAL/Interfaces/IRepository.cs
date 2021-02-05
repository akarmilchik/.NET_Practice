using System;
using System.Linq;

namespace DAL.Interfaces
{
    public interface IRepository
    {
        void Add<TEntity>(TEntity item) where TEntity : class;
        void Dispose();
        IQueryable<TEntity> Get<TEntity>(Func<TEntity, bool> predicate) where TEntity : class;
    }
}