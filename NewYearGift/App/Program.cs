using NewYearGift.App.Constants;
using NewYearGift.Core.Services;
using NewYearGift.DAL.Models.Sweets;
using System;
using System.Collections.Generic;

namespace NewYearGift.App
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;

            int selectedMenuItem;           
           
            Console.WriteLine("Welcome to New Year gift packing, please choose what you would like to do!");

            var localData = JsonDataService.ReadData();

           // var localData = JsonDataService.GetLocalData();

            while (i == 0)
            {
                selectedMenuItem = -1;

                Extensions.PrintMenu();

                var input = Console.ReadKey();

                selectedMenuItem = GiftService.CheckAndConvertInputToInt(input.KeyChar.ToString());

                if (selectedMenuItem >= 0)
                {
                    if (selectedMenuItem == 0)
                    {
                        i = 1;
                        JsonDataService.SaveData(localData);
                    }
                    // Show gift
                    else if (selectedMenuItem == 1)
                    {
                        Extensions.PrintGift(localData.Gift);
                    }
                    // Make new gift
                    else if (selectedMenuItem == 2)
                    {
                        string sweetsRange;

                        List<Sweet> sweets;

                        Extensions.PrintMakeMenu();

                        var inputParams = Console.ReadKey();

                        selectedMenuItem = GiftService.CheckAndConvertInputToInt(inputParams.KeyChar.ToString());

                        Extensions.PrintSweetsMenu();

                        sweets = GiftService.GetSweetsByPresentee(localData.AllSweets, (Presentee)selectedMenuItem);
                        
                        Extensions.PrintSweets(sweets);

                        Extensions.PrintInput();

                        sweetsRange  = Console.ReadLine();

                        var range = sweetsRange.Split(' ');

                        var resultSweets = GiftService.GetSweetsByIndexRange(sweets, range);

                        localData.Gift = GiftService.MakeGift(resultSweets, (Presentee)selectedMenuItem);

                        Extensions.PrintGift(localData.Gift);
                        
                    }
                    // Calc weight of gift
                    else if (selectedMenuItem == 3)
                    {
                        int weight = GiftService.CalculateGiftWeight(localData.Gift);

                        Extensions.PrintGiftWeight(weight);
                    }
                    // Sort sweets in gift by parameter
                    else if (selectedMenuItem == 4)
                    {
                        Extensions.PrintSweetParametersMenu();

                        var inputParams = Console.ReadKey();

                        selectedMenuItem = GiftService.CheckAndConvertInputToInt(inputParams.KeyChar.ToString());

                        List<Sweet> sweets;

                        switch (selectedMenuItem)
                        {
                            case 1:
                                sweets = GiftService.SortSweetsByName(localData.Gift.Sweets, SortOrder.Ascending);
                                Extensions.PrintSweets(sweets);
                                break;
                            case 2:
                                sweets = GiftService.SortSweetsByWeight(localData.Gift.Sweets, SortOrder.Ascending);
                                Extensions.PrintSweets(sweets);
                                break;
                            case 3:
                                sweets = GiftService.SortSweetsByKkal(localData.Gift.Sweets, SortOrder.Ascending);
                                Extensions.PrintSweets(sweets);
                                break;
                            case 4:
                                sweets = GiftService.SortSweetsByFilling(localData.Gift.Sweets, SortOrder.Ascending);
                                Extensions.PrintSweets(sweets);
                                break;
                            case 5:
                                sweets = GiftService.SortSweetsByShape(localData.Gift.Sweets, SortOrder.Ascending);
                                Extensions.PrintSweets(sweets);
                                break;
                        }
                    }
                    // Find sweets in gift by parameter
                    else if (selectedMenuItem == 5)
                    {
                        List<Sweet> findSweets;

                        Extensions.PrintSweetRangeParametersMenu();

                        var inputParams = Console.ReadKey();

                        selectedMenuItem = GiftService.CheckAndConvertInputToInt(inputParams.KeyChar.ToString());

                        Extensions.PrintStartRange();

                        var inputRangeValue = Console.ReadLine();

                        var firstRangeValue = GiftService.CheckAndConvertInputToInt(inputRangeValue);

                        Extensions.PrintEndRange();

                        inputRangeValue = Console.ReadLine();

                        var lastRangeValue = GiftService.CheckAndConvertInputToInt(inputRangeValue);

                        switch (selectedMenuItem)
                        {
                            case 1:
                                findSweets = GiftService.FindSweetsByWeightRange(localData.Gift.Sweets, firstRangeValue, lastRangeValue);
                                Extensions.PrintSweets(findSweets);
                                break;
                            case 2:
                                findSweets = GiftService.FindSweetsByKkalRange(localData.Gift.Sweets, firstRangeValue, lastRangeValue);
                                Extensions.PrintSweets(findSweets);
                                break;
                            case 3:
                                findSweets = GiftService.FindSweetsBySugarRange(localData.Gift.Sweets, firstRangeValue, lastRangeValue);
                                Extensions.PrintSweets(findSweets);
                                break;
                            case 4:
                                findSweets = GiftService.FindSweetsByAlcoholRange(localData.Gift.Sweets, firstRangeValue, lastRangeValue);
                                Extensions.PrintSweets(findSweets);
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
