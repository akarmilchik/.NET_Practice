using System;
using System.Collections.Generic;
using System.Linq;
using TextObjectModel.App.Interfaces;
using TextObjectModel.App.Models;
using TextObjectModel.Core.Interfaces;

namespace TextObjectModel.Core.Services
{
    public class PrintService : IPrintService
    {
        public void PrintWelcome()
        {
            Console.WriteLine("Welcome to Text Object Model creator, please choose what you would like to do!");
        }
        public void PrintMainMenu()
        {
            Console.WriteLine("\nActions:\n 1. Display sentences in ascending of the number of words in each of them \n 2. Find and print without repeating words of a given length in interrogative sentences \n 3. Remove all words of a given length starting with a consonant letter \n 4. In some sentence of the text, replace words of a given length with the specified substring \n 5. Save text object model in txt file \n 0. Close");
            Console.Write("Input: ");
        }

        public void PrintInputWordsLength()
        {
            Console.Write("\nInput words length: ");
        }

        public void PrintConsonantLetterToDeleteWords()
        {
            Console.WriteLine("\nInput consonant letter of the beginning of words to delete:");
            Console.Write("Input: ");
        }

        public void PrintSubstringToReplaceWords()
        {
            Console.WriteLine("\nInput substring to replace in words:");
            Console.Write("Input: ");
        }

        public void PrintNumberOfSentence()
        {
            Console.WriteLine("\nInput number of sentence for replace words:");
            Console.Write("Input: ");
        }

        public void PrintSortingMenu()
        {
            Console.WriteLine("\nChoose sorting order:");
            Console.WriteLine("\n\n 1. Ascending \n 2. Descending\n");
            Console.Write("Input: ");
        }

        public void PrintWrongInput()
        {
            Console.WriteLine("\nWrong input!");
        }

        public void PrintSuccessSave()
        {
            Console.WriteLine("\n\nText object model successfully saved!");
        }

        public void PrintIncorrectChoose()
        {
            Console.WriteLine("\n\nPlease choose correct menu item.");
        }

        public void PrintSentence(ISentence sentence) 
        {
            
            foreach (var sentenceItem in sentence)
            {
                Console.Write(sentenceItem.Chars);
            }

            Console.Write("\n");
        }

        public void PrintSentenceItem(ISentenceItem item)
        {
            Console.WriteLine(item.Chars);
        }

        public void PrintSentencesItems(IEnumerable<ISentenceItem> items)
        {
            Console.WriteLine("\n");
            items.ToList().ForEach(i => PrintSentenceItem(i));
        }

        public void PrintSentencesByOrderOfWords(Text data)
        {
            var sentencesList = data.sentences.ToList();

            sentencesList = sentencesList.OrderBy(s => s.Items.Count()).ToList();

            Console.WriteLine("\n");

            sentencesList.ForEach(s => PrintSentence(s));
        }

        public void PrintSentences(Text data)
        {
            data.sentences.ToList().ForEach(s => PrintSentence(s));
        }
    }
}
