using ATS.DAL.Interfaces;
using System;

namespace ATS.DAL.Models
{
    public class CallDetails : ICallDetails
    {
        public int Id { get; set; }

        public DateTime StartedTime { get; set; }

        public TimeSpan DurationTime { get; set; }

        public string Source { get; set; }

        public string Target { get; set; }

        public decimal Cost { get; set; }

        public override string ToString()
        {
            return $"Date:{StartedTime:G} Duration: {DurationTime}. Cost:{Cost}$. To number:{Target}";
        }
    }
}