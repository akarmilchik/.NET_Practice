using ATS.Core.Interfaces;
using ATS.DAL.Constants;
using System.Linq;

namespace ATS.Core.Services
{
    public class MainMenuService : IMainMenuService
    {
        private readonly IPrintService _printService;
        private readonly IInputService _inputService;
        private readonly IDataService _dataService;

        public MainMenuService(IPrintService printService, IInputService inputService, IDataService dataService)
        {
            _printService = printService;
            _inputService = inputService;
            _dataService = dataService;
        }

        public void ClientMenuHandler()
        {
            var isWorking = true;

            var chosenClientId = 1;

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

                            PrintBasicClientData(chosenClientId);

                            break;

                        case ClientMenuItems.ChooseClientHandle:

                            _printService.PrintLine();

                            _dataService.GetClients().ToList().ForEach(c => _printService.PrintItemValue(c.ToString()));

                            _printService.PrintChooseProposal("client");

                            chosenClientId = _inputService.ReadInputKey();

                            break;

                        case ClientMenuItems.ConnectTerminal:

                            ConnectTerminal(chosenClientId);

                            break;

                        case ClientMenuItems.DisconnectTerminal:

                            DisonnectTerminal(chosenClientId);

                            break;

                        case ClientMenuItems.Call:

                            Call(chosenClientId);

                            break;

                        case ClientMenuItems.DropCall:

                            _dataService.DropCall(chosenClientId);

                            _printService.PrintCallState("dropped");

                            break;

                        case ClientMenuItems.AnswerCall:

                            _dataService.AnswerCall(chosenClientId);

                            _printService.PrintCallState("answered");

                            break;

                        case ClientMenuItems.ShowCallReport:

                            _dataService.CreateReport(chosenClientId);

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
                _printService.PrintItemValue(_dataService.GetClientById(clientId).ToString());

                _printService.PrintItemValue(_dataService.GetContractByClientId(clientId).ToString());

                _printService.PrintItemValue(_dataService.GetPortByClientId(clientId).ToString());

                _printService.PrintItemValue(_dataService.GetTerminalByClientId(clientId).ToString());

                _printService.PrintItemValue(_dataService.GetTariffPlanByClientId(clientId).ToString());
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

        public void Call(int chosenClientId)
        {
            _printService.PrintChooseProposal("terminal");

            _dataService.GetTerminals().ToList().ForEach(t => _printService.PrintItemValue(t.ToString()));

            _printService.PrintInputProposal();

            var targetTerminalId = _inputService.ReadInputKey();

            _dataService.CallToTerminal(chosenClientId, targetTerminalId);

            _printService.PrintCallState("started");
        }

        public void ConnectTerminal(int chosenCliendId)
        {
            _dataService.ConnectTerminalToPort(chosenCliendId);

            _printService.PrintSuccessConnect();
        }

        public void DisonnectTerminal(int chosenCliendId)
        {
            _dataService.DisconnectTerminalFromPort(chosenCliendId);

            _printService.PrintSuccessDisonnect();
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

            _dataService.GetUnmappedTerminals().ToList().ForEach(t => _printService.PrintItemValue(t.ToString()));

            _printService.PrintInputProposal();

            var targetTerminalId = _inputService.ReadInputKey();

            _printService.PrintChooseProposal("Port");

            _printService.PrintLine();

            _dataService.GetUnmappedPorts().ToList().ForEach(p => _printService.PrintItemValue(p.ToString()));

            _printService.PrintInputProposal();

            var targetPortId = _inputService.ReadInputKey();

            _dataService.ConcludeContract(targetClientId, targetPortId, targetTerminalId, clodeDate);

            _printService.PrintLine();

            _printService.ContractConcluded();
        }
    }
}