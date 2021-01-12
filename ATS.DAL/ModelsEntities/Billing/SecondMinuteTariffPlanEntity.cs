namespace ATS.DAL.ModelsEntities.Billing
{
    public class SecondMinuteTariffPlanEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal MinuteCost { get; set; }
        public int CostCalculator_Id { get; set; }
    }
}