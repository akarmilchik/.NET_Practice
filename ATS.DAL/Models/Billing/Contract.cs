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
        public Client Client { get; set; }
        public Terminal Terminal { get; set; }
        public SecondMinuteTariffPlan TariffPlan { get; set; }

        public Contract()
        {
            
        }

        public event EventHandler<Terminal> ContractConcluded;

        public virtual void OnContractConcluded(Terminal terminal)
        {
            ContractConcluded?.Invoke(this, terminal);
        }

        public override string ToString()
        {
            return $"Contract№{Id}\n   Start date: {ContractStartDate:d}\n   Close date: {ContractCloseDate:d}";
        }
    }
}