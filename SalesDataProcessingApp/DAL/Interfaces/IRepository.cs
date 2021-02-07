using System;

namespace DAL.Interfaces
{
    public interface IRepository
    {
        void Add<TEntity>(TEntity item) where TEntity : class;
        void Dispose();
        TEntity Get<TEntity>(Func<TEntity, bool> predicate) where TEntity : class;
    }
}