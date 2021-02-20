using Microsoft.EntityFrameworkCore;
using SalesStatistics.Core.Queries;
using SalesStatistics.DAL;
using SalesStatistics.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesStatistics.Core.Services
{
    public class ClientsService : IClientsService
    {
        private readonly DataContext _context;
        private readonly ISortingProvider<Client> _sortingProvider;

        public ClientsService(DataContext context, ISortingProvider<Client> sortingProvider)
        {
            _context = context;
            _sortingProvider = sortingProvider;
        }

        public ClientsService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<PagedResult<Client>> GetOrders(ClientQuery query)
        {
            var queryable = _context.Clients.AsQueryable();

            var count = await queryable.CountAsync();

            queryable = _sortingProvider.ApplySorting(queryable, query);

            queryable = queryable.ApplyPagination(query);

            var items = await queryable.ToListAsync();

            return new PagedResult<Client> { TotalCount = count, Items = items };
        }


        public async Task<Client> GetClientById(int id)
        {
            return await _context.Clients.FindAsync(id);
        }
    }
}
