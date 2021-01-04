namespace ATEAndBillingSystem.App.Interfaces
{
    public interface IClient
    {
        void ConnectTerminalToPort(IPort port);
        void DisconectTermianlFromPort(IPort port);
    }
}
