using ATS.Core.Interfaces;
using ATS.DAL.Models;

namespace ATS.Core.Services
{
    public class MainMenuService : IMainMenuService
    {
        private readonly IPrintService _printService;

        public MainMenuService(IPrintService printService)
        {
            _printService = printService;
        }

        public void CloseApp()
        {
            _printService.PrintExit();
        }

        public DataModel ShowAllData()
        {
            _printService
        }

        public void OpenClientMenu()
        {
        }

        public void OpenStationMenu()
        {
        }
    }
}