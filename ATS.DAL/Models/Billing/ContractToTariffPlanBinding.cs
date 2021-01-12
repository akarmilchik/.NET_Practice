using ATS.DAL.Interfaces.Billing;
using System;

namespace ATS.DAL.Models.Billing
{
    public class ContractToTariffPlanBinding : IContractToTariffPlanBinding
    {
        public int Id { get; set; }
        public DateTime BindingDate { get; set; }
        public IContract Contract { get; set; }
        public ITariffPlan TariffPlan { get; set; }
    }
}