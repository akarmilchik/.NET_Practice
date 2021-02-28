using System.Collections.Generic;

namespace SalesStatistics.Models
{
    public class ChartViewModel
    {
        public IEnumerable<string> Lables { get; set; }
        public IEnumerable<int> Values { get; set; }
    }
}
