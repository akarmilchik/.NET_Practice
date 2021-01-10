using ATS.DAL.Models;
using System;

namespace ATS.DAL.Interfaces
{
    public interface IStation : IClearEventsService, IEntity
    {
        string Name { get; set; }
        void RegisterEventHandlersForTerminal(ITerminal terminal);

        void RegisterEventHandlersForPort(IPort port);

        event EventHandler<CallDetails> CallDetailsPrepared;
    }
}