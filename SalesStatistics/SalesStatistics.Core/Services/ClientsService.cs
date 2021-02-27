using Microsoft.EntityFrameworkCore;
using SalesStatistics.Core.Queries;
using SalesStatistics.DAL;
using SalesStatistics.DAL.Models;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Client> GetClientById(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<IEnumerable<Client>> GetClientsQuery(ClientQuery query)
        {
            var queryable = _context.Clients.Include(c => c.Country).AsQueryable();

            if (query.Countries != null)
            {
                queryable = queryable.Where(v => query.Countries.Contains(v.CountryId));
            }

            if (query.Ages != "")
            {
                var ages = FormatAges(query.Ages);

                if (ages != null)
                {
                    queryable = queryable.Where(v => v.Age >= ages[0] && v.Age <= ages[1]);
                }
            }

            queryable = _sortingProvider.ApplySorting(queryable, query);

            queryable = queryable.ApplyPagination(query);

            return await queryable.ToListAsync();
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            var queryable = _context.Countries.AsQueryable();

            return await queryable.ToListAsync();
        }

        private List<int> FormatAges(string ages)
        {
            if (ages == null)
            {
                return null;
            }

            return ages.Split('-').Select(int.Parse).ToList();
        }
    }
}
