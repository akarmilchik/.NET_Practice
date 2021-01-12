using ATS.DAL.Interfaces;
using ATS.DAL.Interfaces.Billing;
using ATS.DAL.Models;

namespace ATS.Core.Interfaces
{
    public interface IPrintService
    {
        void PrintChooseClient();

        void PrintClientsMenu();

        void PrintData(IClient user);

        void PrintData(ITerminal terminal);

        void PrintData(IPort port);

        void PrintData(IContract contract);

        void PrintData(IStation station);

        void PrintData(DataModel data);

        void PrintEmptyString();

        void PrintExit();

        void PrintIncorrectChoose();

        void PrintMainMenu();

        void PrintStationMenu();

        void PrintWelcome();
    }
}