using ATS.DAL.Interfaces;
using ATS.DAL.Interfaces.Billing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ATS.DAL.Models.Billing
{
    public class TariffPlan : ITariffPlan
    {
        private ICostCalculator _costCalculator;
        protected byte[] streamObject { get; set; }
        public Guid Id { get; }

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

        public decimal CalculateCallCost(ICallDetails callDetails, IEnumerable<ICallDetails> callsForBillingPeriod)
        {
            return CostCalculator.CalculateCallCost(callDetails, callsForBillingPeriod);
        }
    }
}
