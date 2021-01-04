using ATEAndBillingSystem.App.Interfaces;

namespace ATEAndBillingSystem.App.Models
{
    public class Station : IStation
    {
        public void AssignNumberToAClient(IClient client, PhoneNumber number)
        {
            throw new System.NotImplementedException();
        }

        public void AssignTerminalToAClient(ITerminal terminal, IClient client)
        {
            throw new System.NotImplementedException();
        }

        public void ConcludeContract(IClient client)
        {
            throw new System.NotImplementedException();
        }

        public void ProvidePortForTerminal(ITerminal terminal, IPort port)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterEventHandlersForPort(IPort port)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterEventHandlersForTerminal(ITerminal terminal)
        {
            throw new System.NotImplementedException();
        }
    }
}
