namespace ATS.Core.Interfaces
{
    public interface IMainMenuService
    {
        void ClientMenuHandler();

        void StationMenuHandler();

        void PrintBasicClientData(int clientId);
        void ConcludeContract();
    }
}