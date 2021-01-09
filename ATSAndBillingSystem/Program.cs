using ATS.Core.Extensions;
using ATS.Core.Interfaces;
using ATS.Core.Services;
using ATS.DAL.Constants;
using System;

namespace ATS
{
    class Program
    {
        static void Main()
        {
            bool isWorking = true;

            MainMenuItems mainMenuItems;

            IPrintService printService = new PrintService();

            IMainMenuService mainMenuService = new MainMenuService();

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
                            //printService.PrintSentencesItems();
                            break;
                        case MainMenuItems.OpenStationMenu:
                            mainMenuService.OpenStationMenu();
                            //printService.PrintSentences();
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
