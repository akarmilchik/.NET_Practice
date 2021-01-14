using System.Collections.Generic;

namespace ATS.DAL.Interfaces.Billing
{
    public interface ITariffPlan : IEntity
    { 
        public string Name { get; set; }
        public decimal MinuteCost { get; set; }
        decimal CalculateCallCost(ICallDetails callDetails, IEnumerable<ICallDetails> callsForBillingPeriod);
    }
}