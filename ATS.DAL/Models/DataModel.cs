using ATS.DAL.Interfaces.Billing;
using System.Collections.Generic;

namespace ATS.DAL.Models
{
    public class DataModel
    {
        public IEnumerable<IUser> Clients { get; set; }

    }
}
