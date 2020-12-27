using System;
using System.Collections.Generic;
using System.Linq;
using TextObjectModel.App.Constants;
using TextObjectModel.App.Interfaces;
using TextObjectModel.App.Models;
using TextObjectModel.Core.Extensions;
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

        public IEnumerable<ISentenceItem> FindSentenceItemsInInterrogativeSentences(Text data)
        {
            _printService.PrintInputWordsLength();

            var inputLength = TypeConversionExtension.ToInt(Console.ReadLine());

            var sentenceSeparators = SymbolsContainer.SentenceSeparators().ToList();

            var interrogativeSentences = data.sentences.ToList().Where(s => s.Items.ElementAt(s.Items.Count - 2).Chars == sentenceSeparators[2]);

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

        public Text RemoveSentenceItemsGivenLengthAndStartsConsonantLetter(Text data)
        {
            _printService.PrintInputWordsLength();

            var inputLength = TypeConversionExtension.ToInt(Console.ReadLine());

            var consonantLetters = SymbolsContainer.ConsonantLetters().ToList();

            var inputData = data.sentences.ToList();

            var resultData = data.sentences.ToList();

            for (int i = 0; i < inputData.Count; i++)
            {
                var sentenceItems = inputData[i].Items.ToList();

                for (int j = 0; j < sentenceItems.Count; j++)
                {
                    if (sentenceItems[j].Chars.Length == inputLength && consonantLetters.Contains(sentenceItems[j].Chars[0].ToString()))
                    {
                        resultData.ElementAt(i).Items.Remove(resultData.ElementAt(i).Items.ElementAt(j));
                    }
                }
            }

            return new Text(resultData);
        }

        public ISentence ReplaceSentenceItemsGivenLengthBySubstring(Text data)
        {
            _printService.PrintNumberOfSentence();

            var numberOfSentence = TypeConversionExtension.ToInt(Console.ReadLine());

            _printService.PrintInputWordsLength();

            var inputLength = TypeConversionExtension.ToInt(Console.ReadLine());

            _printService.PrintSubstringToReplaceWords();

            var inputString = Console.ReadLine().Trim();

            if (numberOfSentence < 1)
            {
                numberOfSentence = 1;
            }

            var items = data.sentences.ToList()[numberOfSentence - 1].Items.ToList();

            List<ISentenceItem> resultItems = new List<ISentenceItem>();

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Chars.Length == inputLength)
                {
                    resultItems.Add(new Word(inputString));
                }
                else
                {
                    resultItems.Add(items[i]);
                }

            }

            return new Sentence(resultItems);
        }
    }
}
