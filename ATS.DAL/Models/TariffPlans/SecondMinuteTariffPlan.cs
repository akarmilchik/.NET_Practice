﻿using ATS.DAL.Interfaces;
using ATS.DAL.Interfaces.Billing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATS.DAL.Models.TariffPlans
{
    public class SecondMinuteTariffPlan : ITariffPlan
    { 
        public int Id { get; set; }

        private readonly decimal _minuteCost = 0.01m;

        public string PlanName { get; set; }

        public SecondMinuteTariffPlan(int id, string planName, decimal minuteCost)
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
