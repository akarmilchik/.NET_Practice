using ATS.DAL.Interfaces;
using ATS.DAL.Interfaces.Billing;
using ATS.DAL.Models;
using System.Collections.Generic;

namespace ATS.Core.Interfaces
{
    public interface IPrintService
    {
        void PrintChooseClient();
        void PrintChooseTerminalToCall();
        void PrintClientsMenu();

        void PrintData(IClient client);

        void PrintData(ITerminal terminal);

        void PrintData(IPort port);

        void PrintData(IContract contract);

        void PrintData(IStation station);

        void PrintData(ITariffPlan tariffPlan);

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