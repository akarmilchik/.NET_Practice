using System;

namespace ATS.DAL.Interfaces.Billing
{
    public interface IContract : IEntity
    {
        DateTime ContractStartDate { get; }
        DateTime? ContractCloseDate { get; }
        IUser Client { get; }
        ITerminal Terminal { get; }
    }
}
