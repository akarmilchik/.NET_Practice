using System;
using TextObjectModel.App.Constants;
using TextObjectModel.Core.Services;
using TextObjectModel.DAL.Factories;
using TextObjectModel.DAL.Repositories;

namespace TextObjectModel
{
    class Program
    {
        static void Main(string[] args)
        {
            PunctuationContainer punctuationContainer = new PunctuationContainer();
            WordFactory wordFactory = new WordFactory();
            PunctuationFactory punctuationFactory = new PunctuationFactory(punctuationContainer);
            DataRepository dataRepository = new DataRepository();
            InternService internService = new InternService();
            SentenceItemFactory sentenceItemFactory = new SentenceItemFactory(punctuationFactory, wordFactory, internService);

            Parser parser = new Parser(punctuationContainer, wordFactory, punctuationFactory, dataRepository, sentenceItemFactory, internService);

            parser.Parse();
                
            Console.WriteLine("Worked done!");

            Console.ReadKey();
        }
    }
}
