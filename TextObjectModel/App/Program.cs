using System;
using TextObjectModel.App.Constants;
using TextObjectModel.Core;
using TextObjectModel.Core.Extensions;
using TextObjectModel.Core.Services;
using TextObjectModel.DAL.Factories;
using TextObjectModel.DAL.Repositories;

namespace TextObjectModel
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isWorking = true;

            MenuItems menuItems;

            WordFactory wordFactory = new WordFactory();

            PunctuationFactory punctuationFactory = new PunctuationFactory();

            DataRepository dataRepository = new DataRepository();

            SentenceItemFactory sentenceItemFactory = new SentenceItemFactory(punctuationFactory, wordFactory);

            PrintService printService = new PrintService();

            ParseService parseService = new ParseService();

            Parser parser = new Parser(sentenceItemFactory, parseService);

            var dataPath = dataRepository.GetDataPath();

            var parsedText = parser.Parse(dataPath);

            MenuService menuService = new MenuService(dataRepository, printService, parsedText);

            printService.PrintWelcome();

            while (isWorking)
            {
                printService.PrintMainMenu();

                var input = Console.ReadKey();

                var selectedMenuItemId = TypeConversionExtension.ToInt(input.KeyChar.ToString());

                menuItems = (MenuItems)selectedMenuItemId;

                if (selectedMenuItemId >= 0)
                {
                    switch (menuItems)
                    {
                        case MenuItems.CloseApp:
                            isWorking = false;
                            menuService.CloseApp();
                            break;
                        case MenuItems.DisplayAscOfWords:
                            printService.PrintSentencesByOrderOfWords(parsedText);
                            break;
                        case MenuItems.FindWordsInInterrogativeSentences:
                            var findWords = menuService.FindSentenceItemsInInterrogativeSentences(parsedText);
                            printService.PrintSentencesItems(findWords);
                            break;
                        case MenuItems.RemoveWords:
                            var formattedText = menuService.RemoveSentenceItemsGivenLengthAndStartsConsonantLetter(parsedText);
                            printService.PrintSentences(formattedText);
                            break;
                        case MenuItems.ReplaceWordsBySubstring:
                            var changedSentence = menuService.ReplaceSentenceItemsGivenLengthBySubstring(parsedText);
                            printService.PrintSentence(changedSentence);
                            break;
                        case MenuItems.SaveTextObjectModel:
                            dataRepository.SaveData(parsedText);
                            printService.PrintSuccessSave();
                            break;
                        default:
                            printService.PrintIncorrectChoose();
                            break;
                    }
                }
            }
        }
    }
}
