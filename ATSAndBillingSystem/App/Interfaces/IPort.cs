using ATEAndBillingSystem.App.Constants;
using System;

namespace ATEAndBillingSystem.App.Interfaces
{
    public interface IPort
    {
        PortState State { get; set; }

        event EventHandler<PortState> StateChanging;

        event EventHandler<PortState> StateChanged;
    }
}
