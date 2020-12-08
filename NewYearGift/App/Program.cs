using NewYearGift.Core.Services;
using System;
using System.Text.RegularExpressions;

namespace NewYearGift.App
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;

            Regex regex = new Regex(@"[0-9]");

            Console.WriteLine("Welcome to New Year gift packing, please choose what you would like to do!");


            var data = DataService.ReadData();

            while (i == 0)
            {
                Console.WriteLine("\n\nActions:\n 1. Show current gift \n 2. Make new gift from sweets\n 3. Calculate weight of gift \n 4. Sort sweets in gift by parameter \n 5. Find sweets in gift by parameter \n 0. Close");
                Console.Write("Input: ");
                var res = Console.ReadKey();

                bool sd =  regex.IsMatch(res.ToString());

                if (regex.IsMatch(res.ToString()) && i == 0)
                    i = 1;


            }
        }
    }
}
