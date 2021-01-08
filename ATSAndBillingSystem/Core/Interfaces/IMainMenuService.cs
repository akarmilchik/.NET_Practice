using ATS.App.Models;

namespace ATS.Core.Interfaces
{
    interface IMainMenuService
    {
        void CloseApp();
        void OpenClientMenu();
        void OpenStationMenu();
        DataModel ShowAllData();
    }
}
