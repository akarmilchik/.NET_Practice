using ATS.DAL.Interfaces;
using System;

namespace ATS.DAL.Models
{
    public class CallDetails : ICallDetails
    {
        public DateTime StartedTime { get; set; }
        public TimeSpan DurationTime { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
        public decimal Cost { get; set; }
    }
}