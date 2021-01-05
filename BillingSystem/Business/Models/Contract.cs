using ATS.App.Interfaces;
using BillingSystem.Business.Interfaces;
using System;

namespace BillingSystem.Business.Models
{
    public class Contract: IContract
    {
        public DateTime ContractStartDate
        {
            get;
            set;
        }

        public DateTime? ContractCloseDate
        {
            get;
            set;
        }

        public IUser Client
        {
            get;
            set;
        }

        public ITerminal Terminal
        {
            get;
            set;
        }

        public Guid Id
        {
            get;
            set;
        }
    }
}
