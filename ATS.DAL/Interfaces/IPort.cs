using ATS.DAL.Constants;
using ATS.DAL.Interfaces.Services;
using System;

namespace ATS.DAL.Interfaces
{
    public interface IPort : IClearEventsService, IEntity
    {
        PortState PortState { get; set; }

        event EventHandler<PortState> StateChanging;

        event EventHandler<PortState> StateChanged;

        void RegisterEventHandlersForTerminal(ITerminal terminal);
    }
}