using System;

namespace ATS.DAL.Interfaces.Billing
{
    public interface IContractToTariffPlanBinding : IEntity
    {
        DateTime BindingDate { get; }
        IContract Contract { get; }
        ITariffPlan TariffPlan { get; }
    }
}