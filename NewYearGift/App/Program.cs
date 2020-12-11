using NewYearGift.App.Constants;
using NewYearGift.Core.Services;
using NewYearGift.DAL.Repositories.Interfaces;
using System;

namespace NewYearGift.App
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isWorking = true;

            int selectedMenuItemId = -1;

            MainMenuItems menuItems;

            JsonDataRepository jsonDataRepository = new JsonDataRepository();

            PrintService printService = new PrintService();

            TypeConversionService typeConversionService = new TypeConversionService();

            GiftService giftService = new GiftService();

            var path = jsonDataRepository.GetDataPath();

            var data = jsonDataRepository.ReadData(path);

            MainMenuService mainMenuService = new MainMenuService(jsonDataRepository, typeConversionService, printService, giftService, data, selectedMenuItemId);

            printService.PrintWelcome();

            while (isWorking)
            {
                selectedMenuItemId = -1;

                printService.PrintMainMenu();

                var input = Console.ReadKey();

                selectedMenuItemId = typeConversionService.CheckAndConvertInputToInt(input.KeyChar.ToString());

                menuItems = (MainMenuItems)selectedMenuItemId;

                if (selectedMenuItemId >= 0)
                {
                    switch (menuItems)
                    {
                        case MainMenuItems.CloseApp:
                            isWorking = false;
                            mainMenuService.CloseApp();
                            break;
                        case MainMenuItems.PrintGift:
                            printService.PrintGift(data.Gift);
                            break;
                        case MainMenuItems.MakeNewGift:
                            mainMenuService.MakeNewGift();
                            break;
                        case MainMenuItems.CalcWeightOfGift:
                            int weight = giftService.CalculateGiftWeight(data.Gift);
                            printService.PrintGiftWeight(weight);
                            break;
                        case MainMenuItems.SortGiftSweetsByParameter:
                            mainMenuService.SortGiftSweetsByParameter();
                            break;
                        case MainMenuItems.FindGiftSweetsByParameter:
                            mainMenuService.FindGiftSweetsByParameter();
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
