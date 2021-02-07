using Autofac;
using DAL.Interfaces;
using DAL.IoC;
using System;
using System.Linq;

namespace DAL.Repositories
{
    public class Repository : IRepository, IDisposable
    {
        private DataContext _context;
        private IContainer container;

        public Repository()
        {
            InitContainer();
        }

        public TEntity Get<TEntity>(Func<TEntity, bool> predicate) where TEntity : class
        {
            using (var scope = container.BeginLifetimeScope())
            {
                _context = scope.Resolve<DataContext>();

                var queryable = _context.Set<TEntity>().Where(predicate).AsQueryable();

                return queryable.FirstOrDefault();
            }
        }

        public void Add<TEntity>(TEntity item) where TEntity : class
        {
            using (var scope = container.BeginLifetimeScope())
            {
                _context = scope.Resolve<DataContext>();

                _context.Set<TEntity>().Add(item);

                _context.SaveChanges();
            }
        }

        public void Dispose()
        {
            _context.Dispose();        
        }

        private void InitContainer()
        {
            container = AutofacConfig.ConfigureContainer();
        }
    }
}