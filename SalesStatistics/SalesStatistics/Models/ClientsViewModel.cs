using SalesStatistics.Core.Constants;
using SalesStatistics.DAL.Models;
using System.Collections.Generic;

namespace SalesStatistics.Models
{
    public class ClientsViewModel
    {
        public ICollection<Client> Clients { get; set; }
        public IEnumerable<Country> Countries { get; set; }
        public string Ages { get; set; }
        public ClientSortParams SortBy { get; set; }
        public SortOrder SortOrder { get; set; }
        public int PageSize { get; set; }
    }
}
