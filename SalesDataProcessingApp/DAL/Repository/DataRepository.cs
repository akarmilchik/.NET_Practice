using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class DataRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        DataContext _context;

        DbSet<TEntity> _dbSet;

        public DataRepository(DataContext context)
        {
            _context = context;

            _dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public async Task<TEntity> FindById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Add(TEntity item)
        {
            await _dbSet.AddAsync(item);

            await _context.SaveChangesAsync();
        }

        public async Task Update(TEntity item)
        { 
            _context.Update(item);

            await _context.SaveChangesAsync();
        }

        public async Task Remove(TEntity item)
        {
            _dbSet.Remove(item);

            await _context.SaveChangesAsync();
        }
    }
}
