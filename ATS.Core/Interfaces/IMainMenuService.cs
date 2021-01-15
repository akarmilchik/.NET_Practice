namespace ATS.Core.Interfaces
{
    public interface IMainMenuService
    {
        void ClientMenuHandler();

        void StationMenuHandler();

        void PrintBasicClientData(int clientId);

        void ConcludeContract();

        void ConnectTerminal(int chosenClientId);

        void Call(int chosenClientId);

        void DisonnectTerminal(int chosenClientId);

        void PrintBasicContractsData();
    }
}