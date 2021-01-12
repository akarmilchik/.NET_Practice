using System.Collections.Generic;

namespace ATS.DAL.Interfaces.Billing
{
    public interface ITariffPlan : IEntity
    {
        decimal CalculateCallCost(ICallDetails callDetails, IEnumerable<ICallDetails> callsForBillingPeriod);
    }
}