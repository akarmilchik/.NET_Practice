using System;

namespace ATS.DAL.Interfaces.Billing
{
    public interface IContract : IEntity
    {
        DateTime ContractStartDate { get; }
        DateTime? ContractCloseDate { get; }
        IClient Client { get; }
        ITerminal Terminal { get; }
        ITariffPlan TariffPlan { get; }

        public event EventHandler ContractConclude;
    }
}