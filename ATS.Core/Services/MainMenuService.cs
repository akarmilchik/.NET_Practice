using ATS.Core.Interfaces;
using ATS.DAL;
using ATS.DAL.Constants;
using ATS.DAL.Models;
using System.Linq;

namespace ATS.Core.Services
{
    public class MainMenuService : IMainMenuService
    {
        private readonly IPrintService _printService;
        private readonly IInputService _inputService;
        private readonly IDataService _dataService;
        private readonly DataContext _context;

        public MainMenuService(IPrintService printService, IInputService inputService, IDataService dataService, DataContext context)
        {
            _printService = printService;
            _inputService = inputService;
            _dataService = dataService;
            _context = context;
        }

        public void ClientMenuHandler()
        {
            var isWorking = true;
            var chosenCliendId = 1;

            while (isWorking)
            {
                _printService.PrintClientsMenu();

                var selectedMenuItemId = _inputService.ReadInputKey();

                var clientMenuItem = (ClientMenuItems)selectedMenuItemId;

                if (selectedMenuItemId >= 0)
                {
                    switch (clientMenuItem)
                    {
                        case ClientMenuItems.BackToMain:
                            isWorking = false;
                            break;

                        case ClientMenuItems.ShowCurrentClient:
                            PrintBasicClientData(chosenCliendId);
                            break;

                        case ClientMenuItems.ChooseClientHandle:
                            _printService.PrintLine();

                            _dataService.GetClients().ToList().ForEach(c => _printService.PrintItemValue(c.ToString()));

                            _printService.PrintChooseProposal("client");

                            chosenCliendId = _inputService.ReadInputKey();
                            break;

                        case ClientMenuItems.ConnectTerminal:
                            //switch to online

                            break;

                        case ClientMenuItems.DisconnectTerminal:
                            break;

                        case ClientMenuItems.Call:
                            Call(chosenCliendId);
                            break;

                        case ClientMenuItems.DropCall:
                            break;

                        case ClientMenuItems.AnswerCall:
                            break;

                        case ClientMenuItems.ShowCallReport:
                            break;

                        default:
                            _printService.PrintIncorrectChoose();
                            break;
                    }
                }
            }
        }

        public void StationMenuHandler()
        {
            bool isWorking = true;

            while (isWorking)
            {
                _printService.PrintStationMenu();

                var selectedMenuItemId = _inputService.ReadInputKey();

                var stationMenuItem = (StationMenuItems)selectedMenuItemId;

                if (selectedMenuItemId >= 0)
                {
                    switch (stationMenuItem)
                    {
                        case StationMenuItems.BackToMain:
                            isWorking = false;
                            break;

                        case StationMenuItems.ConcludeAContract:
                            ConcludeContract();
                            break;

                        case StationMenuItems.ShowAllContracts:
                            PrintBasicContractsData();
                            break;

                        default:
                            _printService.PrintIncorrectChoose();
                            break;
                    }
                }
            }
        }

        public void PrintBasicClientData(int clientId)
        {
            if (clientId != 0)
            {
                _dataService.GetClientById(clientId).ToString();
                _dataService.GetContractByClientId(clientId).ToString();
                _dataService.GetPortByClientId(clientId).ToString();
                _dataService.GetTerminalByClientId(clientId).ToString();
                _dataService.GetTariffPlanByClientId(clientId).ToString();
            }
            else 
            {
                _printService.PrintIncorrectChoose();
            }
        }

        public void PrintBasicContractsData()
        {
            _dataService.GetContracts().ToList().ForEach(c => _printService.PrintItemValue(c.ToString()));
        }


        public void Call(int chosenCliendId)
        {
            _printService.PrintChooseProposal("terminal");

            _dataService.GetTerminals().ToList().ForEach(t => _printService.PrintItemValue(t.ToString()));

            _printService.PrintInputProposal();

            var targetTerminalId = _inputService.ReadInputKey();

            _dataService.ConnectToTerminal(chosenCliendId, targetTerminalId);
        }

        public void ConcludeContract()
        {
            _printService.PrintInputCloseDate();

            var clodeDate = _inputService.ReadInputDate();

            _printService.PrintChooseProposal("client");

            _printService.PrintLine();

            _dataService.GetClients().ToList().ForEach(c => _printService.PrintItemValue(c.ToString()));

            _printService.PrintInputProposal();

            var targetClientId = _inputService.ReadInputKey();

            _printService.PrintChooseProposal("terminal");

            _printService.PrintLine();

            _dataService.GetTerminals().ToList().ForEach(t => _printService.PrintItemValue(t.ToString()));

            _printService.PrintInputProposal();

            var targetTerminalId = _inputService.ReadInputKey();

            _printService.PrintChooseProposal("Port");

            _printService.PrintLine();

            _dataService.GetPorts().ToList().ForEach(p => _printService.PrintItemValue(p.ToString()));

            _printService.PrintInputProposal();

            var targetPortId = _inputService.ReadInputKey();

            _dataService.ConcludeContract(targetClientId, targetPortId, targetTerminalId, clodeDate);

            _printService.ContractConcluded();
        }
    }
}