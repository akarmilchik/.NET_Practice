using ATS.DAL.Interfaces.Services;
using ATS.DAL.Models;
using ATS.DAL.Models.Billing;
using System;

namespace ATS.DAL.Interfaces
{
    public interface IStation : IClearEventsService, IEntity
    {
        string Name { get; set; }

        void RegisterEventHandlersForTerminal(ITerminal terminal);

        void RegisterEventHandlersForPort(IPort port);
        void onTerminateContract(object sender, Contract contract);

        event EventHandler<CallDetails> CallDetailsPrepared;

        event EventHandler<Contract> TerminateContract;
    }
}