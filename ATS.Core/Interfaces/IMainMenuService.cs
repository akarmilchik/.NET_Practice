using ATS.DAL.Models;

namespace ATS.Core.Interfaces
{
    public interface IMainMenuService
    {
        void CloseApp();

        void OpenClientMenu();

        void OpenStationMenu();

        void ShowAllData(DataModel model);
    }
}