using ATS.DAL.Interfaces;
using ATS.DAL.Interfaces.Billing;
using System.Collections.Generic;

namespace ATS.Core.Interfaces
{
    public interface IPrintService
    {
        void PrintChooseClient();
        void PrintChooseClientToConcludeContract();
        void PrintChooseTerminalToCall();
        void PrintClientsMenu();

        void PrintDataArray(IEnumerable<IClient> clients);
        void PrintDataArray(IEnumerable<ITerminal> terminals);
        void PrintExit();

        void PrintIncorrectChoose();
        void PrintInputProposal();
        void PrintMainMenu();

        void PrintStationMenu();

        void PrintWelcome();
    }
}