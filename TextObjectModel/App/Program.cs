using System;
using TextObjectModel.App.Constants;
using TextObjectModel.App.Models;
using TextObjectModel.Core;
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

            int selectedMenuItemId = -1;

            MainMenuItems menuItems;


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
                selectedMenuItemId = -1;

                printService.PrintMainMenu();

                var input = Console.ReadKey();

                selectedMenuItemId = TypeConversionService.ToInt(input.KeyChar.ToString());

                menuItems = (MainMenuItems)selectedMenuItemId;

                if (selectedMenuItemId >= 0)
                {
                    switch (menuItems)
                    {
                        case MainMenuItems.CloseApp:
                            isWorking = false;
                            menuService.CloseApp();
                            break;
                        case MainMenuItems.DisplayAscOfWords:
                            printService.PrintSentencesByOrderOfWords(parsedText);
                            break;
                        case MainMenuItems.FindWordsInInterrogativeSentences:
                            var findWords = menuService.FindWordsInInterrogativeSentences(parsedText);
                            printService.PrintSentencesItems(findWords);
                            break;
                        case MainMenuItems.RemoveWords:
                            Text formattedText = menuService.RemoveWordsGivenLengthAndStartsConsonantLetter(parsedText);
                            printService.PrintSentences(formattedText);
                            break;
                        case MainMenuItems.ReplaceWordsBySubstring:
                            Text replacedText = menuService.ReplaceWordsGivenLengthBySubstring(parsedText);
                            printService.PrintSentences(replacedText);
                            break;
                        case MainMenuItems.SaveTextObjectModel:
                            dataRepository.SaveData(parsedText);
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
