using NewYearGift.App.Constants;
using NewYearGift.App.Interfaces;
using NewYearGift.App.Models.Sweets;
using System;
using System.Collections.Generic;

namespace NewYearGift.Models.Gifts
{
    public class Gift : IGift
    {
        public int Weight { get; set; }
        public int Kkal { get; set; }
        public int CountOfSweets { get; set; }
        public IEnumerable<Sweet> Sweets { get; set; }
        public Presentee BelongToPresentee { get; set; }

        public void PrintData()
        {
            Console.WriteLine("\n\nCurrent packed gift:\n");
            Console.WriteLine($"Belong to: {BelongToPresentee}");
            Console.WriteLine($"Weight: {Weight.ToString()} grams");
            Console.WriteLine($"Calorie: {Kkal} kkal");
            Console.WriteLine($"Count of sweets: {CountOfSweets}");
            Console.WriteLine("Sweets:");
        }
    }
}
