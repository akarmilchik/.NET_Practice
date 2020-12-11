using NewYearGift.App.Constants;
using NewYearGift.Core.Services;
using NewYearGift.Core.Services.Interfaces;
using NewYearGift.DAL.Repositories.Interfaces;
using System;

namespace NewYearGift.App
{
    class Program
    {
        private readonly IJsonDataRepository jsonDataRepository;
        private readonly ITypeConversionService typeConversionService;
        private readonly IPrintService printService;
        private readonly IGiftService giftService;
        private readonly IMainMenuService mainMenuService;

        public Program(IJsonDataRepository jsonDataRepository, ITypeConversionService typeConversionService, IPrintService printService, IGiftService giftService, IMainMenuService mainMenuService)
        {
            this.jsonDataRepository = jsonDataRepository;
            this.typeConversionService = typeConversionService;
            this.printService = printService;
            this.giftService = giftService;
            this.mainMenuService = mainMenuService;
        }

        static void Main(string[] args)
        {
            int i = 0;

            int selectedMenuItemId = -1;

            MainMenuItems menuItems;

            Console.WriteLine("Welcome to New Year gift packing, please choose what you would like to do!");

            JsonDataRepository jsonDataRepository = new JsonDataRepository();

            PrintService printService = new PrintService();

            TypeConversionService typeConversionService = new TypeConversionService();

            GiftService giftService = new GiftService();

            var data = jsonDataRepository.ReadData();

            MainMenuService mainMenuService = new MainMenuService(jsonDataRepository, typeConversionService, printService, giftService, data, selectedMenuItemId);

            while (i == 0)
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
                            i = 1;
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
                            Console.WriteLine("\n\nPlease choose correct menu item.");
                            break;
                    }
                }
            }
        }
    }
}
