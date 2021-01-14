using ATS.Core.Interfaces;
using ATS.DAL;
using ATS.DAL.Constants;

namespace ATS.Core.Services
{
    public class MainMenuService : IMainMenuService
    {
        private readonly IPrintService _printService;
        private readonly IInputService _inputService;
        private readonly IDataService _dataService;
        private readonly DataContext _context;
        private int chosenClientId = 1;


        public MainMenuService(IPrintService printService, IInputService inputService, IDataService dataService, DataContext context)
        {
            _printService = printService;
            _inputService = inputService;
            _dataService = dataService;
            _context = context;
        }

        public void ClientMenuHandler()
        {
            bool isWorking = true;

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
                            //PrintBasicData();
                            break;

                        case ClientMenuItems.ChooseClientHandle:
                            _printService.PrintDataArray(_dataService.GetClients());
                            _printService.PrintChooseClient();
                            chosenClientId = _inputService.ReadInputKey();
                            break;

                        case ClientMenuItems.ConnectTerminal:
                            _printService.PrintChooseTerminalToCall();
                            _printService.PrintDataArray(_dataService.GetTerminals());
                            _printService.PrintInputProposal();
                            _inputService.ReadInputKey();
                            break;

                        case ClientMenuItems.DisconnectTerminal:
                            break;

                        case ClientMenuItems.Call:
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
                            break;

                        default:
                            _printService.PrintIncorrectChoose();
                            break;
                    }
                }
            }
        }

        public void PrintBasicData()
        {
            _printService.PrintData(_dataService.GetClientById(chosenClientId));
            _printService.PrintData(_dataService.GetContractByClientId(chosenClientId));
            _printService.PrintData(_dataService.GetPortByClientId(chosenClientId));
            _printService.PrintData(_dataService.GetTerminalByClientId(chosenClientId));
            _printService.PrintData(_dataService.GetTariffPlanByClientId(chosenClientId));
        }
    }
}