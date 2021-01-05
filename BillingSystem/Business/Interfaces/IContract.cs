using ATS.App.Interfaces;
using System;

namespace BillingSystem.Business.Interfaces
{
    public interface IContract : IEntity
    {
        DateTime ContractStartDate { get; }
        DateTime? ContractCloseDate { get; }
        IUser Client { get; }
        ITerminal Terminal { get; }
    }
}
