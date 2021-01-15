using ATS.DAL.Interfaces;
using ATS.DAL.Interfaces.Billing;
using System;

namespace ATS.DAL.Models.Billing
{
    public class Contract : IContract
    {
        public int Id { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractCloseDate { get; set; }
        public IClient Client { get; set; }
        public ITerminal Terminal { get; set; }
        public ITariffPlan TariffPlan { get; set; }

        public Contract()
        {
        }

        public event EventHandler ContractConclude;

        protected virtual void OnContractConclude(EventArgs e)
        {
            ContractConclude?.Invoke(this, e);
        }

        public override string ToString()
        {
            return $"   Contract\n      Start date: {ContractStartDate:d}\n      Close date: {ContractCloseDate:d}\n      Client:{Client.FirstName} {Client.LastName}";
        }
        
    }
}