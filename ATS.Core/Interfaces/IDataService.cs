using ATS.DAL.Interfaces;
using ATS.DAL.Interfaces.Billing;
using ATS.DAL.ModelsEntities.Billing;
using System.Collections.Generic;

namespace ATS.Core.Interfaces
{
    public interface IDataService
    {
        void AddContractToDb(ContractEntity contract);
        void RemoveContractToDb(ContractEntity contract);
        IClient GetClientById(int clientId);
        IEnumerable<IClient> GetClients();
        IEnumerable<IContract> GetContracts();
        IContract GetContractByClientId(int clientId);
        IPort GetPortByClientId(int clientId);
        ITerminal GetTerminalByClientId(int clientId);
        int GetTariffPlanIdByClientId(int clientId);
        ITariffPlan GetTariffPlanByClientId(int clientId);
        IEnumerable<ITerminal> GetTerminals();
    }
}