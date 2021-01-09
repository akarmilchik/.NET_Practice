using System;

namespace ATS.DAL.Interfaces.Billing
{
    public interface IContractToTariffPlanBinding
    {
        DateTime BindingDate { get; }
        IContract Contract { get; }
        ITariffPlan TariffPlan { get; }
    }
}