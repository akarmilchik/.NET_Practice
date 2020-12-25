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
        private readonly ITypeConversionService _typeConversionService;
        private readonly IPrintService _printService;
        private readonly IDataRepository _dataRepository;
        private readonly DataObjectModel _dataObjectModel;
        private SymbolsContainer symbolsContainer = new SymbolsContainer();

        private int _selectedMenuItemId;

        public MenuService(IDataRepository dataRepository, ITypeConversionService typeConversionService, IPrintService printService, int selectedMenuItemId, DataObjectModel dataObjectModel)
        {
            this._dataRepository = dataRepository;
            this._typeConversionService = typeConversionService;
            this._printService = printService;
            this._selectedMenuItemId = selectedMenuItemId;
            this._dataObjectModel = dataObjectModel;
        }
        public void CloseApp()
        {
            _dataRepository.SaveData(_dataObjectModel);
        }

        public IEnumerable<ISentenceItem> FindWordsInInterrogativeSentences(Text data)
        {
            _printService.PrintInputWordsLength();

            var inputLength = _typeConversionService.CheckAndConvertInputToInt(Console.ReadLine());

            var sentenceSeparators = symbolsContainer.SentenceSeparators().ToList();

            var interrogativeSentences = data.sentences.ToList().Where(s => s.items.Last().Chars == sentenceSeparators[0]);

            List<ISentenceItem> resultSentencesItems = new List<ISentenceItem>();

            foreach (var sentence in interrogativeSentences)
            {
                foreach (var sentenceItem in sentence.items)
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

            var inputLength = _typeConversionService.CheckAndConvertInputToInt(Console.ReadLine());

            var consonantLetters = symbolsContainer.ConsonantLetters().ToList();

            foreach (var sentence in data.sentences)
            {
                foreach (var sentenceItem in sentence.items)
                {
                    if (sentenceItem.Chars.Length == inputLength && consonantLetters.Contains(sentenceItem.Chars[0].ToString()))
                    {
                        sentence.items.Remove(sentenceItem);
                    }
                }
            }

            return data;
        }

        public Text ReplaceWordsGivenLengthBySubstring(Text data)
        {
            _printService.PrintNumberOfSentence();

            var numberOfSentence = _typeConversionService.CheckAndConvertInputToInt(Console.ReadLine());

            _printService.PrintInputWordsLength();

            var inputLength = _typeConversionService.CheckAndConvertInputToInt(Console.ReadLine());

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
