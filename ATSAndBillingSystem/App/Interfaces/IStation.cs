using ATS.App.Models;
using System;

namespace ATS.App.Interfaces
{
    public interface IStation
    {
        void RegisterEventHandlersForTerminal(ITerminal terminal);
        void RegisterEventHandlersForPort(IPort port);

        event EventHandler<CallDetails> CallDetailsPrepared;
    }
}
