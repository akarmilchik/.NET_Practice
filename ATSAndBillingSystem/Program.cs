using ATS.Core.Interfaces;
using ATS.Core.Services;
using ATS.DAL.Constants;
using ATS.Helpers;
using System;
using System.Linq;

namespace ATS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isWorking = true;

            var contextFactory = new DataContextFactory();

            var context = contextFactory.CreateDbContext(args);

            context.Database.EnsureCreated();

            InitData.InitializeData(context);

            MainMenuItems mainMenuItems;

            IPrintService printService = new PrintService();

            IInputService inputService = new InputService();

            IDataService dataService = new DataService(context);

            IMainMenuService mainMenuService = new MainMenuService(printService, inputService, dataService);

            while (isWorking)
            {
                printService.PrintWelcome();

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

                        case MainMenuItems.PrintClientsData:
                            printService.PrintLine();
                            dataService.GetClients().ToList().ForEach(c => printService.PrintItemValue(c.ToString()));
                            break;

                        case MainMenuItems.OpenStationMenu:
                            mainMenuService.StationMenuHandler();
                            break;

                        case MainMenuItems.OpenClientMenu:
                            mainMenuService.ClientMenuHandler();
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