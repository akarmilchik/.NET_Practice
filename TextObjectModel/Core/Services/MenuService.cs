using System;
using System.Collections.Generic;
using System.Linq;
using TextObjectModel.App.Constants;
using TextObjectModel.App.Interfaces;
using TextObjectModel.App.Models;
using TextObjectModel.Core.Interfaces;
using TextObjectModel.DAL.Repositories.Interfaces;

namespace TextObjectModel.Core.Services
{
    class MenuService : IMenuService
    {
        private readonly IPrintService _printService;
        private readonly IDataRepository _dataRepository;
        private readonly Text _textData;
        public MenuService(IDataRepository dataRepository, IPrintService printService, Text textData)
        {
            _dataRepository = dataRepository;
            _printService = printService;
            _textData = textData;
        }
        public void CloseApp()
        {
            _dataRepository.SaveData(_textData);
        }

        public IEnumerable<ISentenceItem> FindWordsInInterrogativeSentences(Text data)
        {
            _printService.PrintInputWordsLength();

            var inputLength = TypeConversionService.ToInt(Console.ReadLine());

            var sentenceSeparators = SymbolsContainer.SentenceSeparators().ToList();

            var interrogativeSentences = data.sentences.ToList().Where(s => s.Items.Last().Chars == sentenceSeparators[0]);

            List<ISentenceItem> resultSentencesItems = new List<ISentenceItem>();

            foreach (var sentence in interrogativeSentences)
            {
                foreach (var sentenceItem in sentence.Items)
                {
                    if (sentenceItem.Chars.Length == inputLength)
                    {
                        resultSentencesItems.Add(sentenceItem);
                    }
                }
            }

            return resultSentencesItems;
        }

        public Text RemoveWordsGivenLengthAndStartsConsonantLetter(Text data)
        {
            _printService.PrintInputWordsLength();

            var inputLength = TypeConversionService.ToInt(Console.ReadLine());

            var consonantLetters = SymbolsContainer.ConsonantLetters().ToList();

            foreach (var sentence in data.sentences)
            {
                foreach (var sentenceItem in sentence.Items)
                {
                    if (sentenceItem.Chars.Length == inputLength && consonantLetters.Contains(sentenceItem.Chars[0].ToString()))
                    {
                        sentence.Items.Remove(sentenceItem);
                    }
                }
            }

            return data;
        }

        public Text ReplaceWordsGivenLengthBySubstring(Text data)
        {
            _printService.PrintNumberOfSentence();

            var numberOfSentence = TypeConversionService.ToInt(Console.ReadLine());

            _printService.PrintInputWordsLength();

            var inputLength = TypeConversionService.ToInt(Console.ReadLine());

            _printService.PrintSubstringToReplaceWords();

            var inputString = Console.ReadLine().Trim();
           
            foreach (var sentenceItem in data.sentences.ToList()[numberOfSentence - 1])
            {
                if (sentenceItem.Chars.Length == inputLength)
                {
                    sentenceItem.Chars = inputString;
                }
            }

            return data;
        }
    }
}
