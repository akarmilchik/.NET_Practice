using ATS.DAL.Models;
using System;

namespace ATS.DAL.Interfaces
{
    public interface IStation : IClearEventsService, IEntity
    {
        void RegisterEventHandlersForTerminal(ITerminal terminal);

        void RegisterEventHandlersForPort(IPort port);

        event EventHandler<CallDetails> CallDetailsPrepared;
    }
}