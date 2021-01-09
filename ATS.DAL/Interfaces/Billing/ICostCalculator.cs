using System.Collections.Generic;

namespace ATS.DAL.Interfaces.Billing
{
    public interface ICostCalculator
    {
        decimal CalculateCallCost(ICallDetails callDetails, IEnumerable<ICallDetails> callsForBillingPeriod);
    }
}