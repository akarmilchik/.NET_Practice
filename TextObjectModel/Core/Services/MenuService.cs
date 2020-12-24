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
        private readonly Text _parsedText;
        private readonly DataObjectModel _dataObjectModel;
        private SymbolsContainer punctuationContainer = new SymbolsContainer();

        private int _selectedMenuItemId;

        public MenuService(IDataRepository dataRepository, ITypeConversionService typeConversionService, IPrintService printService, Text parsedText, int selectedMenuItemId, DataObjectModel dataObjectModel)
        {
            this._dataRepository = dataRepository;
            this._typeConversionService = typeConversionService;
            this._printService = printService;
            this._parsedText = parsedText;
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

            var sentenceSeparators = punctuationContainer.SentenceSeparators().ToList();

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

        public void RemoveWordsGivenLength(Text data)
        {
            _printService.PrintInputWordsLength();

            var inputLength = _typeConversionService.CheckAndConvertInputToInt(Console.ReadLine());

            var sentences = data.sentences.ToList().Where(s => s);

        }


    }
}
