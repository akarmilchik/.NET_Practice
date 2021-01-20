namespace ATS.Core.Interfaces
{
    public interface IMainMenuService
    {
        void ClientMenuHandler();

        void StationMenuHandler();

        void PrintBasicClientData(int clientId);

        void ConcludeContract();

        void ConnectTerminalToPort(int chosenClientId);

        void Call(int chosenClientId);

        void DisonnectTerminalFromPort(int chosenClientId);

        void PrintBasicContractsData();

        void PrintReport(int chosenClientId);
    }
}