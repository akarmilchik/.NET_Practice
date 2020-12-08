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
            int menuCheck = -1;

            Console.WriteLine("Welcome to New Year gift packing, please choose what you would like to do!");

            var data = DataService.ReadData2();

            while (i == 0)
            {
                Extensions.PrintMenu();

                var res = Console.ReadKey();

                var isNumber = Extensions.CheckInputForNumber(res.KeyChar.ToString());

                if (isNumber && Convert.ToInt32(res) == 0)
                {
                    i = 1;
                }
                

            }
        }
    }
}
