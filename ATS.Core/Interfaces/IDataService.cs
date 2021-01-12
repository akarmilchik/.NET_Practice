using ATS.DAL.Models.Billing;
using ATS.DAL.ModelsEntities.Billing;
using System.Collections.Generic;

namespace ATS.Core.Interfaces
{
    public interface IDataService
    {
        void AddContractToDb(ContractEntity contract);
        List<Client> GetClients();
        List<Contract> GetContracts();
        List<ContractToTariffPlanBinding> GetContractsBindings();
        void RemoveContractToDb(ContractEntity contract);
    }
}