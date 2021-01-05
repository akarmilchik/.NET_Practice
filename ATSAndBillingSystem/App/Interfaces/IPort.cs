using ATS.App.Constants;
using System;

namespace ATS.App.Interfaces
{
    public interface IPort
    {
        PortState PortState { get; set; }

        event EventHandler<PortState> StateChanging;

        event EventHandler<PortState> StateChanged;

        void RegisterEventHandlersForTerminal(ITerminal terminal);
    }
}
