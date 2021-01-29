using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entityItem);

        Task<TEntity> FindById(int id);

        Task<IEnumerable<TEntity>> Get();

        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);

        Task Remove(TEntity item);

        void Update(TEntity item);
    }
}
