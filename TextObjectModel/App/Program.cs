using System;
using TextObjectModel.App.Constants;
using TextObjectModel.Core.Services;

namespace TextObjectModel
{
    class Program
    {
        static void Main(string[] args)
        {
            PunctuationContainer PunctuationContainer = new PunctuationContainer();

            Parser parser = new Parser(PunctuationContainer);

            parser.Parse();
                

            Console.WriteLine("Worked done!");

            Console.ReadKey();
        }
    }
}
