using SalesStatistics.Core.Queries;
using SalesStatistics.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesStatistics.Core.Services
{
    public interface IClientsService
    {
        Task<Client> GetClientById(int id);
        Task<List<Client>> GetClients();
        Task<PagedResult<Client>> GetOrders(ClientQuery query);
    }
}
