using ATS.DAL.Interfaces;
using ATS.DAL.Interfaces.Billing;
using ATS.DAL.Models.Billing;
using ATS.DAL.ModelsEntities.Billing;
using System;
using System.Collections.Generic;

namespace ATS.Core.Interfaces
{
    public interface IDataService
    {
        void AddContractToDb(Contract contract);
        void ConcludeContract(int clientId, int portId, int terminalId, DateTime closeDate);
        void CallToTerminal(int chosenCliendId, int targetTerminalId);
        IClient GetClientById(int clientId);
        IEnumerable<IClient> GetClients();
        IContract GetContractByClientId(int clientId);
        IContract GetContractById(int contractId);
        IEnumerable<IContract> GetContracts();
        IPort GetPortByClientId(int clientId);
        int GetPortIdByTerminalId(int terminalId);
        IEnumerable<IPort> GetPorts();
        ITariffPlan GetTariffPlan();
        ITariffPlan GetTariffPlanByClientId(int clientId);
        int GetTariffPlanIdByClientId(int clientId);
        ITerminal GetTerminalByClientId(int clientId);
        ITerminal GetTerminalById(int terminalId);
        int GetTerminalIdByClientId(int clientId);
        string GetTerminalPhoneNumberById(int terminalId);
        IEnumerable<ITerminal> GetTerminals();
        void RemoveContractFromDb(Contract contract);
        void ConnectTerminalToPort(int chosenCliendId);
        void DisconnectTerminalFromPort(int chosenCliendId);
        void DropCall(int chosenClientId);
        void AnswerCall(int chosenCliendId);
        void CreateReport(int chosenClientId);
        IEnumerable<ITerminal> GetUnmappedTerminals();
        IEnumerable<IPort> GetUnmappedPorts();
    }
}