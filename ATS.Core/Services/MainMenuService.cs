using ATS.Core.Interfaces;
using ATS.Core.Mapper;
using ATS.DAL;
using ATS.DAL.Constants;

namespace ATS.Core.Services
{
    public class MainMenuService : IMainMenuService
    {
        private readonly IPrintService _printService;
        private readonly IInputService _inputService;
        private readonly DataContext _context;

        public MainMenuService(IPrintService printService, IInputService inputService, DataContext context)
        {
            _printService = printService;
            _inputService = inputService;
            _context = context;
        }

        public void OpenClientMenu()
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
                            break;

                        case ClientMenuItems.ChooseClient:
                            break;

                        case ClientMenuItems.ConnectTerminal:
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

        public void OpenStationMenu()
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

        public void ShowAllData()
        {
            //_printService.PrintData();
        }
    }
}