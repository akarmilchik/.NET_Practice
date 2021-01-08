using ATS.App.Interfaces;
using System.Collections.Generic;

namespace BillingSystem.Business.Interfaces
{
    public interface ICostCalculator
    {
        decimal CalculateCallCost(ICallDetails callDetails, IEnumerable<ICallDetails> callsForBillingPeriod);
    }
}
