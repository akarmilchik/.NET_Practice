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

        private decimal _minuteCost = 0.1m;

        public string Name { get; set; }

        public decimal MinuteCost
        {
            get { return _minuteCost; }
            set { _minuteCost = value; }
        }

        public SecondMinuteTariffPlan()
        {
        }

        public SecondMinuteTariffPlan(int id, string planName, decimal minuteCost)
        {
            Id = id;
            Name = planName;
            _minuteCost = minuteCost;
        }

        public decimal CalculateCallCost(ICallDetails callDetails, IEnumerable<ICallDetails> callsForBillingPeriod)
        {
            TimeSpan sum = new TimeSpan();

            callsForBillingPeriod.Aggregate(sum, (seed, x) => seed + x.DurationTime);

            return (decimal)(sum.TotalMinutes + callDetails.DurationTime.TotalMinutes) * _minuteCost;
        }

        public override string ToString()
        {
            return $"   Tariff plan\n      Name: {Name}\n      Minute cost: {_minuteCost}$";
        }
    }
}