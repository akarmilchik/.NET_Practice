namespace ATS.Core.Interfaces
{
    public interface IPrintService
    {
        void ContractConcluded();
        void PrintCallState(string state);
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
        void PrintSuccessConnect();
        void PrintSuccessDisonnect();
        void PrintWelcome();
    }
}