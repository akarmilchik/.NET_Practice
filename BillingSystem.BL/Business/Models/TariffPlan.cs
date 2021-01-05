using ATS.App.Interfaces;
using BillingSystem.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BillingSystem.Business.Models
{
    public class TariffPlan: ITariffPlan
    {
        private ICostCalculator _costCalculator;
        public ICostCalculator CostCalculator
        {
            protected get
            {/*
                if (_costCalculator == null)
                {
                    if (this.boxedObj != null)
                    {
                        using (var stream = new MemoryStream(this.boxedObj))
                        {
                            var serializer = new BinaryFormatter();
                            _costCalculator = serializer.Deserialize(stream) as ICostCalculator;
                        }
                    }
                }*/
                return _costCalculator;
            }
            set
            {/*
                if (value != null)
                {
                    using (var stream = new MemoryStream())
                    {
                        var serializer = new BinaryFormatter();
                        serializer.Serialize(stream, value);
                        var length = stream.Length;
                        boxedObj = stream.ToArray();
                    }
                }*/
            }
        }

        public Guid Id { get; }

        public decimal CalculateCallCost(ICallDetails callDetails, IEnumerable<ICallDetails> callsForBillingPeriod)
        {
            return CostCalculator.CalculateCallCost(callDetails, callsForBillingPeriod);
        }

    }
}
