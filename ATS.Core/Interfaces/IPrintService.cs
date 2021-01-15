namespace ATS.Core.Interfaces
{
    public interface IPrintService
    {
        void ContractConcluded();
        void PrintChooseProposal(string value);
        void PrintClientsMenu();
        void PrintExit();
        void PrintIncorrectChoose();
        void PrintInputCloseDate();
        void PrintInputProposal();
        void PrintItemValue(string value);
        void PrintLine();
        void PrintMainMenu();
        void PrintStationMenu();
        void PrintWelcome();
    }
}