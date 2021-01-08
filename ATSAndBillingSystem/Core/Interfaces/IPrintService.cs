namespace ATS.Core.Interfaces
{
    public interface IPrintService
    {
        void PrintChooseClient();
        void PrintClient(IUser client);
        void PrintClientsMenu();
        void PrintEmptyString();
        void PrintIncorrectChoose();
        void PrintMainMenu();
        void PrintNumberOfSentence();
        void PrintStationMenu();
        void PrintSuccessSave();
        void PrintWelcome();
    }
}
