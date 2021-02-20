using SalesStatistics.DAL.Models;
using System.Collections.Generic;

namespace SalesStatistics.Models
{
    public class ClientsViewModel
    {
        public ICollection<Client> Clients { get; set; }
    }
}
