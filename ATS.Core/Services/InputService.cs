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
            return DateTime.Parse(Console.ReadLine());
        }
    }
}