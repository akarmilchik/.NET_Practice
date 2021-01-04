using ATEAndBillingSystem.App.Models;

namespace ATEAndBillingSystem.App.Interfaces
{
    public interface IStation
    {
        void ConcludeContract(IClient client);
        void AssignNumberToAClient(IClient client, PhoneNumber number);
        void ProvidePortForTerminal(ITerminal terminal, IPort port);
        void AssignTerminalToAClient(ITerminal terminal, IClient client);
        void RegisterEventHandlersForTerminal(ITerminal terminal);
        void RegisterEventHandlersForPort(IPort port);

    }
}
