using DAL.Interfaces;
using System;
using System.Linq;

namespace DAL.Repositories
{
    public class Repository : IRepository, IDisposable
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> Get<TEntity>(Func<TEntity, bool> predicate) where TEntity : class
        {
            return _context.Set<TEntity>().Where(predicate).AsQueryable();
        }

        public void Add<TEntity>(TEntity item) where TEntity : class
        {
            _context.Set<TEntity>().Add(item);

            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();        
        }
    }
}
