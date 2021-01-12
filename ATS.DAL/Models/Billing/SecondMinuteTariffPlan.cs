using ATS.DAL.Interfaces;
using ATS.DAL.Interfaces.Billing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATS.DAL.Models.Billing
{
    public class SecondMinuteTariffPlan : ITariffPlan
    {
        public int Id { get; set; }

        private readonly decimal _minuteCost = 0.01m;

        public string Name { get; set; }

        public decimal MinuteCost
        {
            get { return _minuteCost; }
            set { MinuteCost = _minuteCost; }
        }

        public SecondMinuteTariffPlan()
        {
        }

        public SecondMinuteTariffPlan(int id, string planName, decimal minuteCost)
        {
            Id = id;
            _minuteCost = minuteCost;
            Name = planName;
        }

        public decimal CalculateCallCost(ICallDetails callDetails, IEnumerable<ICallDetails> callsForBillingPeriod)
        {
            TimeSpan sum = new TimeSpan();

            callsForBillingPeriod.Aggregate(sum, (seed, x) => seed + x.DurationTime);

            return (decimal)(sum.TotalMinutes + callDetails.DurationTime.TotalMinutes) * _minuteCost;
        }
    }
}