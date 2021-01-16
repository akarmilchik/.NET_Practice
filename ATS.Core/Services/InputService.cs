using ATS.Core.Interfaces;
using ATS.Core.Extensions;
using System;

namespace ATS.Core.Services
{
    public class InputService : IInputService
    {
        public string ReadInputLine()
        {
            return Console.ReadLine();
        }

        public int ReadInputKey()
        {
            return Console.ReadKey().KeyChar.ToInt();
        }

        public DateTime ReadInputDate()
        {
            try
            {
                return DateTime.Parse(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Enter invalid date: {e.Message}");
                return new DateTime(9999, 1, 1);
            }
        }
    }
}