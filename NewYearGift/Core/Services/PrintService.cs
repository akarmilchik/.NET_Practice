using NewYearGift.App.Interfaces;
using NewYearGift.Core.Services.Interfaces;
using NewYearGift.DAL.Models.Sweets;
using NewYearGift.Models.Gifts;
using System;
using System.Collections.Generic;

namespace NewYearGift.Core.Services
{
    public class PrintService : IPrintService
    {
        private readonly IGift gift;
        private readonly Sweet sweet;
        public PrintService(IPrintService printService, IGift gift, Sweet sweet)
        {
            this.gift = gift;
            this.sweet = sweet;
        }

        public void PrintMainMenu()
        {
            Console.WriteLine("\nActions:\n 1. Show current gift \n 2. Make new gift from sweets\n 3. Calculate weight of gift \n 4. Sort sweets in gift by parameter \n 5. Find sweets in gift by parameter \n 0. Close");
            Console.Write("Input: ");
        }

        public void PrintSweetParametersMenu()
        {
            Console.WriteLine("\n\nSweet parameters:\n\n 1. Name \n 2. Weight\n 3. Calorie \n 4. Filling \n 5. Shape\n");
            Console.Write("Input: ");
        }

        public void PrintSweetRangeParametersMenu()
        {
            Console.WriteLine("\n\nSweet parameters:\n\n 1. Weight \n 2. Calorie \n 3. Sugar weight \n 4. Alcohol degree\n");
            Console.Write("Input: ");
        }

        public void PrintStartRangeText()
        {
            Console.Write("\nInput range start value:");

        }

        public void PrintEndRangeText()
        {
            Console.Write("\nInput range end value:");

        }

        public void PrintInputText()
        {
            Console.Write("\nInput numbers separated by a space: ");

        }

        public void PrintChoosePresenteeMenu()
        {
            Console.WriteLine("\nChoose who the gift is for:");
            Console.WriteLine("\n\nPresentee:\n\n 1. Children \n 2. Adult\n");
            Console.Write("Input: ");
        }

        public void PrintSortingMenu()
        {
            Console.WriteLine("\nChoose sorting order:");
            Console.WriteLine("\n\n 1. Ascending \n 2. Descending\n");
            Console.Write("Input: ");
        }

        public void PrintSweetsMenu()
        {
            Console.WriteLine("\nChoose sweets for gift:\n");
        }
        public void PrintGiftWeight(int weight)
        {
            Console.WriteLine("\n\nGift weight: {0} gramms", weight);
        }

        public void PrintGift(Gift gift)
        {
            Console.WriteLine("\n\nCurrent packed gift:\n");
            Console.WriteLine(@"Belong to: {0}", gift.BelongToPresentee);
            Console.WriteLine(@"Weight: {0} grams", gift.Weight);
            Console.WriteLine(@"Calorie: {0} kkal", gift.Kkal);
            Console.WriteLine(@"Count of sweets: {0}", gift.CountOfSweets);
            Console.WriteLine("Sweets:");

            foreach (Sweet sweet in gift.Sweets)
            {
                Console.WriteLine(@"    Name: {0}", sweet.Name);
                Console.WriteLine(@"    Weight: {0} grams", sweet.Weight);
                Console.WriteLine($"    Calorie: {sweet.Kkal} kkal");
                Console.WriteLine(@"    Filling: {0}", sweet.Filling.ToString());
                Console.WriteLine(@"    Shape: {0}", sweet.Shape.Name);

                if (sweet is SugarSweet)
                {
                    Console.WriteLine(@"    Sugar weight: {0}", (sweet as SugarSweet).SugarWeight);
                }
                else if (sweet is AlcoholicSweet)
                {
                    Console.WriteLine(@"    Alcohol: {0} degrees", (sweet as AlcoholicSweet).AlcoholDegree);
                }
                else if (sweet is AlcoholicSugarSweet)
                {
                    Console.WriteLine(@"    Sugar weight: {0}", (sweet as AlcoholicSugarSweet).SugarWeight);
                    Console.WriteLine(@"    Alcohol: {0} degrees", (sweet as AlcoholicSugarSweet).AlcoholDegree);
                }
                Console.WriteLine("\n");
            }
        }

        public void PrintSweets(List<Sweet> sweets)
        {
            Console.WriteLine("\n");
            for(int i = 0; i < sweets.Count; i++)
            {
                Console.WriteLine(@"№ {0}", i + 1);
                Console.WriteLine(@"    Name: {0}", sweets[i].Name);
                Console.WriteLine(@"    Weight: {0} grams", sweets[i].Weight);
                Console.WriteLine(@"    Calorie: {0} kkal", sweets[i].Kkal);
                Console.WriteLine(@"    Filling: {0}", sweets[i].Filling.Name);
                Console.WriteLine(@"    Shape: {0}", sweets[i].Shape.Name);

                if (sweets[i] is SugarSweet)
                {
                    Console.WriteLine(@"    Sugar weight: {0}", (sweets[i] as SugarSweet).SugarWeight);
                }
                else if (sweets[i] is AlcoholicSweet)
                {
                    Console.WriteLine(@"    Alcohol: {0} degrees", (sweets[i] as AlcoholicSweet).AlcoholDegree);
                }
                else if (sweets[i] is AlcoholicSugarSweet)
                {
                    Console.WriteLine(@"    Sugar weight: {0}", (sweets[i] as AlcoholicSugarSweet).SugarWeight);
                    Console.WriteLine(@"    Alcohol: {0} degrees", (sweets[i] as AlcoholicSugarSweet).AlcoholDegree);
                }
                Console.WriteLine("\n");
            }
            if (sweets.Count == 0)
            {
                Console.WriteLine("Nothing to print");
            }
        }
    }
}
