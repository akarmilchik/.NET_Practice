using ATS.DAL.Constants;
using System;

namespace ATS.DAL.Interfaces
{
    public interface IPort : IDisposable
    {
        int Id { get; set; }

        PortState PortState { get; set; }

        event EventHandler<PortState> StateChanged;

        event EventHandler<PortState> StateChanging;

        void RegisterEventHandlersForTerminal(ITerminal terminal);
    }
}