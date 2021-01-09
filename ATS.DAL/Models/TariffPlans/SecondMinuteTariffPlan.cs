using ATS.DAL.Interfaces;
using ATS.DAL.Interfaces.Billing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace ATS.DAL.Models.TariffPlans
{
    public class SecondMinuteTariffPlan : ITariffPlan
    {
        public int Id { get; set; }

        private readonly decimal _minuteCost = 0.01m;

        private ICostCalculator _costCalculator;

        protected byte[] streamObject { get; set; }

        public string PlanName { get; set; }

        public ICostCalculator CostCalculator
        {
            protected get
            {
                if (_costCalculator == null)
                {
                    if (this.streamObject != null)
                    {
                        using (var stream = new MemoryStream(this.streamObject))
                        {
                            var serializer = new BinaryFormatter();

                            _costCalculator = serializer.Deserialize(stream) as ICostCalculator;
                        }
                    }
                }
                return _costCalculator;
            }
            set
            {
                if (value != null)
                {
                    using (var stream = new MemoryStream())
                    {
                        var serializer = new BinaryFormatter();

                        serializer.Serialize(stream, value);

                        var length = stream.Length;

                        streamObject = stream.ToArray();
                    }
                }
            }
        }

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