using NewYearGift.App.Constants;
using NewYearGift.Core.Services;
using NewYearGift.Core.Services.Interfaces;
using NewYearGift.DAL.Models.Sweets;
using NewYearGift.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace NewYearGift.App
{
    class Program
    {
        private readonly IJsonDataRepository jsonDataRepository;
        private readonly ITypeConversionService typeConversionService;
        private readonly IPrintService printService;
        private readonly IGiftService giftService;

        public Program(IJsonDataRepository jsonDataRepository, ITypeConversionService typeConversionService, IPrintService printService, IGiftService giftService)
        {
            this.jsonDataRepository = jsonDataRepository;
            this.typeConversionService = typeConversionService;
            this.printService = printService;
            this.giftService = giftService;
        }

        void Main(string[] args)
        {
            int i = 0;

            int selectedMenuItem;

            Console.WriteLine("Welcome to New Year gift packing, please choose what you would like to do!");

            var localData = jsonDataRepository.ReadData(); 

            while (i == 0)
            {
                selectedMenuItem = -1;

                printService.PrintMainMenu();

                var input = Console.ReadKey();

                selectedMenuItem = typeConversionService.CheckAndConvertInputToInt(input.KeyChar.ToString());

                if (selectedMenuItem >= 0)
                {
                    if (selectedMenuItem == 0)
                    {
                        i = 1;
                        var path = jsonDataRepository.GetDataPath();
                        jsonDataRepository.SaveData(localData, path);
                    }
                    // Show gift
                    else if (selectedMenuItem == 1)
                    {
                        printService.PrintGift(localData.Gift);
                    }
                    // Make new gift
                    else if (selectedMenuItem == 2)
                    {
                        string sweetsRange;

                        List<Sweet> sweets;

                        printService.PrintChoosePresenteeMenu();

                        var inputParams = Console.ReadKey();

                        selectedMenuItem = typeConversionService.CheckAndConvertInputToInt(inputParams.KeyChar.ToString());

                        printService.PrintSweetsMenu();

                        sweets = giftService.GetSweetsByPresentee(localData.AllSweets, (Presentee)selectedMenuItem);

                        printService.PrintSweets(sweets);

                        printService.PrintInputText();

                        sweetsRange  = Console.ReadLine();

                        var range = sweetsRange.Split(' ');

                        var intRange = typeConversionService.CheckAndConvertInputArrayToInt(range);

                        var resultSweets = giftService.GetSweetsByIndexRange(sweets, intRange);

                        localData.Gift = giftService.MakeGift(resultSweets, (Presentee)selectedMenuItem);

                        printService.PrintGift(localData.Gift);
                        
                    }
                    // Calc weight of gift
                    else if (selectedMenuItem == 3)
                    {
                        int weight = giftService.CalculateGiftWeight(localData.Gift);

                        printService.PrintGiftWeight(weight);
                    }
                    // Sort sweets in gift by parameter
                    else if (selectedMenuItem == 4)
                    {
                        printService.PrintSweetParametersMenu();

                        var inputParams = Console.ReadKey();

                        printService.PrintSortingMenu();

                        var inputSorting = Console.ReadKey();

                        var inputSortingInt = typeConversionService.CheckAndConvertInputToInt(inputSorting.KeyChar.ToString());

                        var sortOrder = (SortOrder)inputSortingInt;

                        selectedMenuItem = typeConversionService.CheckAndConvertInputToInt(inputParams.KeyChar.ToString());

                        List<Sweet> sweets;

                        switch (selectedMenuItem)
                        {
                            case 1:
                                sweets = giftService.SortSweetByString(s => s.Name, localData.Gift.Sweets, sortOrder);
                                printService.PrintSweets(sweets);
                                break;
                            case 2:
                                sweets = giftService.SortSweetByInt(s => s.Weight, localData.Gift.Sweets, sortOrder);
                                printService.PrintSweets(sweets);
                                break;
                            case 3:
                                sweets = giftService.SortSweetByInt(s => s.Kkal, localData.Gift.Sweets, sortOrder);
                                printService.PrintSweets(sweets);
                                break;
                            case 4:
                                sweets = giftService.SortSweetByString(s => s.Filling.Name, localData.Gift.Sweets, sortOrder);
                                printService.PrintSweets(sweets);
                                break;
                            case 5:
                                sweets = giftService.SortSweetByString(s => s.Shape.Name, localData.Gift.Sweets, sortOrder);
                                printService.PrintSweets(sweets);
                                break;
                        }
                    }
                    // Find sweets in gift by parameter
                    else if (selectedMenuItem == 5)
                    {
                        List<Sweet> findSweets;

                        printService.PrintSweetRangeParametersMenu();

                        var inputParams = Console.ReadKey();

                        selectedMenuItem = typeConversionService.CheckAndConvertInputToInt(inputParams.KeyChar.ToString());

                        printService.PrintStartRangeText();

                        var inputRangeValue = Console.ReadLine();

                        var firstRangeValue = typeConversionService.CheckAndConvertInputToInt(inputRangeValue);

                        printService.PrintEndRangeText();

                        inputRangeValue = Console.ReadLine();

                        var lastRangeValue = typeConversionService.CheckAndConvertInputToInt(inputRangeValue);

                        switch (selectedMenuItem)
                        {
                            case 1:
                                findSweets = giftService.GetSweetsByWeightRange(localData.Gift.Sweets, firstRangeValue, lastRangeValue);
                                printService.PrintSweets(findSweets);
                                break;
                            case 2:
                                findSweets = giftService.GetSweetsByKkalRange(localData.Gift.Sweets, firstRangeValue, lastRangeValue);
                                printService.PrintSweets(findSweets);
                                break;
                            case 3:
                                findSweets = giftService.FindSweetsBySugarRange(localData.Gift.Sweets, firstRangeValue, lastRangeValue);
                                printService.PrintSweets(findSweets);
                                break;
                            case 4:
                                findSweets = giftService.FindSweetsByAlcoholRange(localData.Gift.Sweets, firstRangeValue, lastRangeValue);
                                printService.PrintSweets(findSweets);
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n\nPlease choose correct menu item.");
                    }
                }
            }
        }
    }
}
