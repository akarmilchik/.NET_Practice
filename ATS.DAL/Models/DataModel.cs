using ATS.DAL.Interfaces;
using ATS.DAL.Interfaces.Billing;
using System.Collections.Generic;

namespace ATS.DAL.Models
{
    public class DataModel
    {
        public IEnumerable<IClient> Clients { get; set; }
        public IEnumerable<ITerminal> Terminals { get; set; }
        public IEnumerable<IPort> Ports { get; set; }
        public IEnumerable<IContract> Contracts { get; set; }
        public IEnumerable<IStation> Stations { get; set; }
    }
}