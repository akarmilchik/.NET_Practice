using System;

namespace BillingSystem.Business.Interfaces
{
    public interface IContractToTariffPlanBinding
    {
        DateTime BindingDate { get; }
        IContract Contract { get; }
        ITariffPlan TariffPlan { get; }
    }
}
