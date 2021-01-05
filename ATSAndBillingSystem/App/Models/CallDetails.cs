using ATS.App.Interfaces;
using System;

namespace ATS.App.Models
{
    public class CallDetails : ICallDetails
    {
        public DateTime StartedTime { get; set; }
        public TimeSpan DurationTime { get; set; }
        public ITerminal Source { get; set; }
        public ITerminal Target { get; set; }
        public decimal Cost { get; set; }
    }
}
