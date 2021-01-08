using ATS.DAL.Constants;
using System;

namespace ATS.DAL.Interfaces
{
    public interface IPort : IClearEventsService
    {
        PortState PortState { get; set; }
        event EventHandler<PortState> StateChanging;
        event EventHandler<PortState> StateChanged;
        void RegisterEventHandlersForTerminal(ITerminal terminal);
    }
}
