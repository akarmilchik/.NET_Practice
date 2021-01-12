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

        public void ShowAllData(DataModel model)
        {
            _printService.PrintData(model);
        }

        public void OpenClientMenu()
        {
        }

        public void OpenStationMenu()
        {
        }
    }
}