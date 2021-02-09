using ATS.DAL.Models;
using ATS.DAL.Models.Billing;
using System;

namespace ATS.DAL.Interfaces.Billing
{
    public interface IContract
    {
        int Id { get; set; }
        DateTime ContractStartDate { get; }
        DateTime ContractCloseDate { get; }
        Client Client { get; }
        Terminal Terminal { get; }
        SecondMinuteTariffPlan TariffPlan { get; }

        public event EventHandler<Terminal> ContractConcluded;
    }
}