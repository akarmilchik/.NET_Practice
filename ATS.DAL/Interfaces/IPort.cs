using ATS.DAL.Constants;
using System;

namespace ATS.DAL.Interfaces
{
    public interface IPort : IEntity, IDisposable
    {
        PortState PortState { get; set; }

        event EventHandler<PortState> StateChanged;

        event EventHandler<PortState> StateChanging;

        void RegisterEventHandlersForTerminal(ITerminal terminal);
    }
}