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

            SymbolsContainer symbolsContainer = new SymbolsContainer();

            WordFactory wordFactory = new WordFactory();

            PunctuationFactory punctuationFactory = new PunctuationFactory(symbolsContainer);

            DataRepository dataRepository = new DataRepository();

            InternService internService = new InternService();

            SentenceItemFactory sentenceItemFactory = new SentenceItemFactory(punctuationFactory, wordFactory, internService);

            PrintService printService = new PrintService();

            TypeConversionService typeConversionService = new TypeConversionService();

            ParseService parseService = new ParseService();

            Parser parser = new Parser(symbolsContainer, sentenceItemFactory, internService, parseService);

            Text parsedText = parseService.ParseData(parser, dataRepository);

            var dataObjectModel = dataRepository.ReadData();

            MenuService menuService = new MenuService(dataRepository, typeConversionService, printService, dataObjectModel);

            printService.PrintWelcome();

            while (isWorking)
            {
                selectedMenuItemId = -1;

                printService.PrintMainMenu();

                var input = Console.ReadKey();

                selectedMenuItemId = typeConversionService.CheckAndConvertInputToInt(input.KeyChar.ToString());

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
                            dataObjectModel = dataRepository.UpdateObjectModel(parsedText);
                            dataRepository.SaveData(dataObjectModel);
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
