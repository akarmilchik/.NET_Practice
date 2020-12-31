using System;
using TextObjectModel.App.Constants;
using TextObjectModel.Core;
using TextObjectModel.Core.Extensions;
using TextObjectModel.Core.Interfaces;
using TextObjectModel.Core.Services;
using TextObjectModel.DAL.Factories;
using TextObjectModel.DAL.Factories.Interfaces;
using TextObjectModel.DAL.Repositories;
using TextObjectModel.DAL.Repositories.Interfaces;

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

            IDataRepository dataRepository = new DataRepository();

            ISentenceItemFactory sentenceItemFactory = new SentenceItemFactory(punctuationFactory, wordFactory);

            IPrintService printService = new PrintService();

            IParseService parseService = new ParseService();

            Parser parser = new Parser(sentenceItemFactory, parseService);

            var dataPath = dataRepository.GetDataPath();

            var parsedText = parser.Parse(dataPath);

            IMenuService menuService = new MenuService(dataRepository, printService, parsedText);

            printService.PrintWelcome();

            while (isWorking)
            {
                printService.PrintMainMenu();

                var input = Console.ReadKey();

                var selectedMenuItemId = input.KeyChar.ToString().ToInt();

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
