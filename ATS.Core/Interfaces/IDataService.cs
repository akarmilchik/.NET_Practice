using ATS.DAL.Interfaces;
using ATS.DAL.Interfaces.Billing;
using System;
using System.Collections.Generic;

namespace ATS.Core.Interfaces
{
    public interface IDataService
    {
        void ConcludeContract(int clientId, int portId, int terminalId, DateTime closeDate);

        void CallToTerminal(int chosenClientId, int targetTerminalId);

        IClient GetClientById(int clientId);

        IEnumerable<IClient> GetClients();

        IContract GetContractByClientId(int clientId);

        IEnumerable<IContract> GetContracts();

        IPort GetPortByClientId(int clientId);

        int GetPortIdByTerminalId(int terminalId);

        ITariffPlan GetTariffPlanByClientId(int clientId);

        int GetTariffPlanIdByClientId(int clientId);

        ITerminal GetTerminalByClientId(int clientId);

        ITerminal GetTerminalById(int terminalId);

        int GetTerminalIdByClientId(int clientId);

        string GetTerminalPhoneNumberById(int terminalId);

        void ConnectTerminalToPort(int chosenClientId);

        void DisconnectTerminalFromPort(int chosenClientId);

        void DropCall(int chosenClientId);

        void AnswerCall(int chosenClientId);

        IEnumerable<ITerminal> GetUnmappedTerminals();

        IEnumerable<IPort> GetUnmappedPorts();

        ITerminal GetTerminalByPhoneNumber(string number);

        IEnumerable<ICallDetails> GetCallDetailsInPeriod(int chosenClientId, DateTime startReportDay, DateTime lastReportDay);

        decimal CalculateMonthCallCost(int chosenClientId, IEnumerable<ICallDetails> callsDetails);

        IEnumerable<ICallDetails> GetCallDetailsByPhoneNumberAndPeriod(string phoneNumber, DateTime startDate, DateTime lastDate);

        void DropOutgoingCall(int chosenClientId, int sourceTerminalId);

        IEnumerable<ITariffPlan> GetTariffPlans();
    }
}