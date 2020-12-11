using NewYearGift.App.Constants;
using NewYearGift.App.Models;
using NewYearGift.App.Models.Sweets;
using NewYearGift.Core.Services.Interfaces;
using NewYearGift.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace NewYearGift.Core.Services
{
    public class MainMenuService : IMainMenuService
    {
        private readonly IJsonDataRepository jsonDataRepository;
        private readonly ITypeConversionService typeConversionService;
        private readonly IPrintService printService;
        private readonly IGiftService giftService;
        private readonly JsonDataModel data;
        private int selectedMenuItemId;

        public MainMenuService(IJsonDataRepository jsonDataRepository, ITypeConversionService typeConversionService, IPrintService printService, IGiftService giftService, JsonDataModel data, int selectedMenuItemId)
        {
            this.jsonDataRepository = jsonDataRepository;
            this.typeConversionService = typeConversionService;
            this.printService = printService;
            this.giftService = giftService;
            this.data = data;
            this.selectedMenuItemId = selectedMenuItemId;
        }

        public void CloseApp()
        {
            var path = jsonDataRepository.GetDataPath();

            jsonDataRepository.SaveData(data, path);
        }

        public void MakeNewGift()
        {
            string sweetsRange;

            IEnumerable<Sweet> sweets;

            printService.PrintChoosePresenteeMenu();

            var inputParams = Console.ReadKey();

            selectedMenuItemId = typeConversionService.CheckAndConvertInputToInt(inputParams.KeyChar.ToString());

            printService.PrintSweetsMenu();

            sweets = giftService.GetSweetsByPresentee(data.AllSweets, (Presentee)selectedMenuItemId);

            printService.PrintSweets(sweets);

            printService.PrintInputText();

            sweetsRange = Console.ReadLine();

            var range = sweetsRange.Split(' ');

            var intRange = typeConversionService.CheckAndConvertInputArrayToInt(range);

            var resultSweets = giftService.GetSweetsByIndexRange(sweets, intRange);

            data.Gift = giftService.MakeGift(resultSweets, (Presentee)selectedMenuItemId);

            printService.PrintGift(data.Gift);
        }

        public void SortGiftSweetsByParameter()
        {
            IEnumerable<Sweet> sweets;

            printService.PrintSweetParametersMenu();

            var inputParams = Console.ReadKey();

            printService.PrintSortingMenu();

            var inputSorting = Console.ReadKey();

            var inputSortingInt = typeConversionService.CheckAndConvertInputToInt(inputSorting.KeyChar.ToString());

            var sortOrder = (SortOrder)inputSortingInt;

            selectedMenuItemId = typeConversionService.CheckAndConvertInputToInt(inputParams.KeyChar.ToString());

            switch (selectedMenuItemId)
            {
                case 1:
                    sweets = giftService.SortSweetByString(s => s.Name, data.Gift.Sweets, sortOrder);
                    printService.PrintSweets(sweets);
                    break;
                case 2:
                    sweets = giftService.SortSweetByInt(s => s.Weight, data.Gift.Sweets, sortOrder);
                    printService.PrintSweets(sweets);
                    break;
                case 3:
                    sweets = giftService.SortSweetByInt(s => s.Kkal, data.Gift.Sweets, sortOrder);
                    printService.PrintSweets(sweets);
                    break;
                case 4:
                    sweets = giftService.SortSweetByString(s => s.Filling.Name, data.Gift.Sweets, sortOrder);
                    printService.PrintSweets(sweets);
                    break;
                case 5:
                    sweets = giftService.SortSweetByString(s => s.Shape.Name, data.Gift.Sweets, sortOrder);
                    printService.PrintSweets(sweets);
                    break;
            }
        }

        public void FindGiftSweetsByParameter()
        {
            IEnumerable<Sweet> findSweets;

            printService.PrintSweetRangeParametersMenu();

            var inputParams = Console.ReadKey();

            selectedMenuItemId = typeConversionService.CheckAndConvertInputToInt(inputParams.KeyChar.ToString());

            printService.PrintStartRangeText();

            var inputRangeValue = Console.ReadLine();

            var firstRangeValue = typeConversionService.CheckAndConvertInputToInt(inputRangeValue);

            printService.PrintEndRangeText();

            inputRangeValue = Console.ReadLine();

            var lastRangeValue = typeConversionService.CheckAndConvertInputToInt(inputRangeValue);
            /*
            switch (selectedMenuItemId)
            {
                case 1:
                    findSweets = giftService.GetSweetsByWeightRange(data.Gift.Sweets, firstRangeValue, lastRangeValue);
                    printService.PrintSweets(findSweets);
                    break;
                case 2:
                    findSweets = giftService.GetSweetsByKkalRange(data.Gift.Sweets, firstRangeValue, lastRangeValue);
                    printService.PrintSweets(findSweets);
                    break;
                case 3:
                    findSweets = giftService.FindSweetsBySugarRange(data.Gift.Sweets, firstRangeValue, lastRangeValue);
                    printService.PrintSweets(findSweets);
                    break;
                case 4:
                    findSweets = giftService.FindSweetsByAlcoholRange(data.Gift.Sweets, firstRangeValue, lastRangeValue);
                    printService.PrintSweets(findSweets);
                    break;
            }*/
        }
    }
}
