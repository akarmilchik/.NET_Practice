using ATS.Core.Interfaces;
using ATS.Core.Mapper;
using ATS.Core.Services;
using ATS.DAL.Constants;
using ATS.Helpers;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ATS
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            bool isWorking = true;

            MainMenuItems mainMenuItems;

            //var mapper = MapperFactory.InitMapper();

            IPrintService printService = new PrintService();

            IInputService inputService = new InputService();

            var contextFactory = new DataContextFactory();

            var context = contextFactory.CreateDbContext(args);

            context.Database.EnsureCreated();

            IDataService dataService = new DataService(context);

            IMainMenuService mainMenuService = new MainMenuService(printService, inputService, dataService, context);

            InitData.InitializeData(context);

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

                        case MainMenuItems.PrintBasicData:
                            printService.PrintDataArray(dataService.GetClients());
                            break;

                        case MainMenuItems.OpenClientMenu:
                            mainMenuService.ClientMenuHandler();
                            break;

                        case MainMenuItems.OpenStationMenu:
                            mainMenuService.StationMenuHandler();
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