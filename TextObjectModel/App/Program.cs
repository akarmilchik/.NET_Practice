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
            bool isWorking = true;

            int selectedMenuItemId = -1;

            MainMenuItems menuItems;

            PunctuationContainer punctuationContainer = new PunctuationContainer();
            WordFactory wordFactory = new WordFactory();
            PunctuationFactory punctuationFactory = new PunctuationFactory(punctuationContainer);
            DataRepository dataRepository = new DataRepository();
            InternService internService = new InternService();
            SentenceItemFactory sentenceItemFactory = new SentenceItemFactory(punctuationFactory, wordFactory, internService);
            
            PrintService printService = new PrintService();
            TypeConversionService typeConversionService = new TypeConversionService();

            Parser parser = new Parser(punctuationContainer, wordFactory, punctuationFactory, dataRepository, sentenceItemFactory, internService);

            printService.PrintWelcome();

            var path = dataRepository.GetDataPath();

            var parsedText = parser.Parse(path);

            MenuService menuService = new MenuService(dataRepository, typeConversionService, printService, parsedText, selectedMenuItemId);


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
                            //printService.PrintGift(data.Gift);
                            break;
                        case MainMenuItems.FindWordsInInterrogativeSentences:
                            //menuService.MakeNewGift();
                            break;
                        case MainMenuItems.RemoveWordGivenLength:
                            //int weight = giftService.CalculateGiftWeight(data.Gift);
                            //printService.PrintGiftWeight(weight);
                            break;
                        case MainMenuItems.ReplaceWordsBySubstring:
                            //menuService.SortGiftSweetsByParameter();
                            break;
                        case MainMenuItems.SaveTextObjectModel:
                            //menuService.FindGiftSweetsByParameter();
                            break;
                        default:
                            printService.PrintIncorrectChoose();
                            break;
                    }
                }
            }
            

            Console.ReadKey();
        }
    }
}
