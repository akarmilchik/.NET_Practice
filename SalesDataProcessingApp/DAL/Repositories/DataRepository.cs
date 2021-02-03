using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class DataRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context;

        private readonly DbSet<TEntity> _dbSet;

        public DataRepository(DataContext context)
        {
            _context = context;

            _dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public void Add(TEntity item)
        {
             _dbSet.Add(item);

             _context.SaveChanges();
        }

    }
}
