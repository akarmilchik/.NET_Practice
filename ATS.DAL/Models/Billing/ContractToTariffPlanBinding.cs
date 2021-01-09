using ATS.DAL.Interfaces.Billing;
using System;

namespace ATS.DAL.Models.Billing
{
    public class ContractToTariffPlanBinding : IContractToTariffPlanBinding
    {
        public DateTime BindingDate { get; set; }

        public IContract Contract { get; set; }

        public ITariffPlan TariffPlan { get; set; }

        public Guid Id { get; set; }
    }
}