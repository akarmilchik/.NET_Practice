using System;

namespace ATS.DAL.ModelsEntities.Billing
{
    public class ContractEntity
    {
        public int Id { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractCloseDate { get; set; }
        public int Client_Id { get; set; }
        public int Terminal_Id { get; set; }
        public int TariffPlan_ID { get; set; }
    }
}