using System;

namespace ATS.DAL.ModelsEntities.Billing
{
    public class ContractToTariffPlanBindingEntity
    {
        public int Id { get; set; }
        public DateTime BindingDate { get; set; }
        public int Contract_Id { get; set; }
        public int TariffPlan_Id { get; set; }
    }
}