using ATS.App.Interfaces;
using BillingSystem.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BillingSystem.Business.Models.TariffPlans
{
    public class SecondMinuteTariffPlan : ITariffPlan
    {
        private decimal _minuteCost = 0.01m;
        public Guid Id { get; }
        public string PlanName { get; private set; }

        public SecondMinuteTariffPlan(Guid id, string planName, decimal minuteCost)
        {
            Id = id;
            _minuteCost = minuteCost;
            PlanName = planName;
        }

        public decimal CalculateCallCost(ICallDetails callDetails, IEnumerable<ICallDetails> callsForBillingPeriod)
        {
            TimeSpan sum = new TimeSpan();

            callsForBillingPeriod.Aggregate(sum, (seed, x) => seed + x.DurationTime);

            return (decimal)(sum.TotalMinutes + callDetails.DurationTime.TotalMinutes) * _minuteCost;
            
        }
    }
}
