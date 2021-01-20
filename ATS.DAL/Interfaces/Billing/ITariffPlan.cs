using System.Collections.Generic;

namespace ATS.DAL.Interfaces.Billing
{
    public interface ITariffPlan
    {
        int Id { get; set; }
        string Name { get; set; }
        decimal MinuteCost { get; set; }
        decimal CalculateCallCost(IEnumerable<ICallDetails> callsForBillingPeriod);
    }
}