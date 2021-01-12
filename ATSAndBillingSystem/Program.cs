using ATS.Core.Interfaces;
using ATS.Core.Services;
using ATS.DAL.Constants;
using ATS.DAL.Models;
using ATS.Helpers;

namespace ATS
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            bool isWorking = true;

            MainMenuItems mainMenuItems;

            IPrintService printService = new PrintService();

            IInputService inputService = new InputService();

            var contextFactory = new DataContextFactory();

            var context = contextFactory.CreateDbContext(args);

            IMainMenuService mainMenuService = new MainMenuService(printService, inputService, context);

            InitData.InitializeData(context);

            var mapper = MapperFactory.InitMapper();

            while (isWorking)
            {
                printService.PrintMainMenu();

                var selectedMenuItemId = inputService.ReadInputKey();

                mainMenuItems = (MainMenuItems)selectedMenuItemId;

                if (selectedMenuItemId >= 0)
                {
                    switch (mainMenuItems)
                    {
                        case MainMenuItems.CloseApp:
                            isWorking = false;
                            printService.PrintExit();
                            break;

                        case MainMenuItems.ShowAllData:
                            mainMenuService.ShowAllData();
                            break;

                        case MainMenuItems.OpenClientMenu:
                            mainMenuService.OpenClientMenu();
                            break;

                        case MainMenuItems.OpenStationMenu:
                            mainMenuService.OpenStationMenu();
                            break;

                        default:
                            printService.PrintIncorrectChoose();
                            break;
                    }
                }
            }
        }
    }
}