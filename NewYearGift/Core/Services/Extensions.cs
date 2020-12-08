using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NewYearGift.Core.Services
{
    public static class Extensions
    {

        public static void PrintMenu()
        {
            Console.WriteLine("\n\nActions:\n\n 1. Show current gift \n 2. Make new gift from sweets\n 3. Calculate weight of gift \n 4. Sort sweets in gift by parameter \n 5. Find sweets in gift by parameter \n 0. Close\n");
            Console.Write("Input: ");
        }

        public static bool CheckInputForNumber(string keyChar)
        {
            Regex regex = new Regex(@"[0-9]");

            bool result = regex.IsMatch(keyChar);

            return result;
        }
    }
}
