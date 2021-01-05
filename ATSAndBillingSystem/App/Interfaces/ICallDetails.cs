using System;

namespace ATS.App.Interfaces
{
    public interface ICallDetails
    {
        DateTime StartedTime { get; set; }
        TimeSpan DurationTime { get; set; }
        ITerminal Source { get; set; }
        ITerminal Target { get; set; }
        decimal Cost { get; set; }
    }
}
