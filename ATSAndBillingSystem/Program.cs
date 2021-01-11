using ATS.Core.Extensions;
using ATS.Core.Interfaces;
using ATS.Core.Services;
using ATS.DAL.Constants;
using System;

namespace ATS
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            bool isWorking = true;

            MainMenuItems mainMenuItems;

            IPrintService printService = new PrintService();

            IMainMenuService mainMenuService = new MainMenuService(printService);

            //var context = new DataContext();

            //InitData.InitializeData(context);

            while (isWorking)
            {
                printService.PrintMainMenu();

                var input = Console.ReadKey();

                var selectedMenuItemId = input.KeyChar.ToInt();

                mainMenuItems = (MainMenuItems)selectedMenuItemId;

                if (selectedMenuItemId >= 0)
                {
                    switch (mainMenuItems)
                    {
                        case MainMenuItems.CloseApp:
                            isWorking = false;
                            mainMenuService.CloseApp();
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