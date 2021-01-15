using ATS.DAL.Models;
using ATS.DAL.Models.Billing;
using System;

namespace ATS.DAL.Interfaces
{
    public interface IStation : IDisposable
    {
        int Id { get; set; }

        string Name { get; set; }

        event EventHandler<CallDetails> CallDetailsPrepared;

        event EventHandler<Contract> TerminateContract;

        void RegisterEventHandlersForTerminal(ITerminal terminal);

        void RegisterEventHandlersForPort(IPort port);

        void OnTerminateContract(object sender, Contract contract);
    }
}