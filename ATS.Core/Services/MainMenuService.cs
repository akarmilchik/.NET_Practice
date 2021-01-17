using ATS.Core.Interfaces;
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

        public static Station station;

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

                            _printService.PrintLine();

                            if (chosenClientId > 0)
                            {
                                _printService.PrintClientSelected();
                            }

                            break;

                        case ClientMenuItems.ConnectTerminal:

                            ConnectTerminalToPort(chosenClientId);

                            break;

                        case ClientMenuItems.DisconnectTerminal:

                            DisonnectTerminalFromPort(chosenClientId);

                            break;

                        case ClientMenuItems.Call:

                            Call(chosenClientId);

                            break;
                        case ClientMenuItems.DropOutgoingCall:

                            var terminal = _dataService.GetTerminalByClientId(chosenClientId);

                            _dataService.DropOutgoingCall(chosenClientId, terminal.Id);

                            _printService.PrintCallState("dropped");

                            break;

                        case ClientMenuItems.DropCall:

                            _printService.PrintLine();

                            _dataService.DropCall(chosenClientId);

                            _printService.PrintCallState("dropped");

                            break;

                        case ClientMenuItems.AnswerCall:

                            _printService.PrintLine();

                            _dataService.AnswerCall(chosenClientId);

                            _printService.PrintCallState("answered");

                            break;

                        case ClientMenuItems.ShowCallReport:

                            PrintReport(chosenClientId);

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
            _printService.PrintLine();

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
            _printService.PrintLine();
            var contracts =_dataService.GetContracts().ToList();

            foreach (var contract in contracts)
            {
                _printService.PrintItemValue(contract.ToString()); 
                
                var client = _dataService.GetClientById(contract.Client.Id);

                _printService.PrintItemValue(client.ToString());

                _printService.PrintLine();
            }
        }

        public void Call(int chosenClientId)
        {
            _printService.PrintLine();

            _printService.PrintChooseProposal("client");

            var contracts = _dataService.GetContracts().Where(c => c.Client.Id != chosenClientId).AsEnumerable();

            _printService.PrintLine();

            foreach (var contract in contracts)
            {
                _printService.PrintItemValue(_dataService.GetClientById(contract.Client.Id).ToString());

                _printService.PrintItemValue(_dataService.GetTerminalByClientId(contract.Client.Id).ToString());

                _printService.PrintLine();
            }

            _printService.PrintInputProposal();

            var targetTerminalId = _inputService.ReadInputKey();

            _dataService.CallToTerminal(chosenClientId, targetTerminalId);

            _printService.PrintLine();

            _printService.PrintCallState("started");
        }

        public void ConnectTerminalToPort(int chosenClientId)
        {
            _dataService.ConnectTerminalToPort(chosenClientId);

            _printService.PrintLine();

            _printService.PrintSuccessConnect();
        }

        public void DisonnectTerminalFromPort(int chosenClientId)
        {
            _dataService.DisconnectTerminalFromPort(chosenClientId);

            _printService.PrintLine();

            _printService.PrintSuccessDisonnect();
        }

        public void ConcludeContract()
        {
            _printService.PrintInputCloseDate("contract");

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

        public void PrintReport(int chosenClientId)
        {
            _printService.PrintInputStartDate("payment period");

            var startDate = _inputService.ReadInputDate();

            _printService.PrintInputCloseDate("payment period");

            var lastDate = _inputService.ReadInputDate();

            var monthCallDetails = _dataService.GetCallDetailsInPeriod(chosenClientId, startDate, lastDate).ToList().OrderBy(c => c.StartedTime);

            _printService.PrintLine();

            foreach (var details in monthCallDetails)
            {
                _printService.PrintItemValue(details.ToString());
            }

            _printService.PrintLine();

            _printService.PrintTotalCost(monthCallDetails.Sum(c => c.Cost));

            var cost = _dataService.CalculateMonthCallCost(chosenClientId, monthCallDetails);
        }
    }
}