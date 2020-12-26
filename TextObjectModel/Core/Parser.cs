using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TextObjectModel.App.Constants;
using TextObjectModel.App.Interfaces;
using TextObjectModel.App.Models;
using TextObjectModel.Core.Services;
using TextObjectModel.DAL.Factories;
using TextObjectModel.DAL.Repositories.Interfaces;

namespace TextObjectModel.Core
{
    public class Parser
    {
        private readonly SymbolsContainer _symbolsContainer;
        private readonly SentenceItemFactory _sentenceItemFactory;

        private ParseService _parseService;

        public Parser(SymbolsContainer symbolsContainer, SentenceItemFactory sentenceItemFactory, ParseService parseService)
        {
            _symbolsContainer = symbolsContainer;
            _sentenceItemFactory = sentenceItemFactory;
            _parseService = parseService;
        }

        public Text Parse(string dataPath)
        {
            int bufferlength = 10000;

            int separatorOccurence = -1;

            string spaceSeparator = _symbolsContainer.WordSeparators().ToList()[0];

            List<ISentence> workedSentences = new List<ISentence>();

            StringBuilder buffer = new StringBuilder(bufferlength);

            buffer.Clear();

            using (StreamReader reader = new StreamReader(dataPath, Encoding.Default))
            {
                string currentString;

                while ((currentString = reader.ReadLine()) != null)
                {
                    currentString = _parseService.ClearSentenceStringLine(currentString, _symbolsContainer);

                    while (currentString.Length > 0)
                    {
                        string sentenceSeparator = _parseService.FindSeparator(currentString, spaceSeparator, ref separatorOccurence, _symbolsContainer);

                        if (sentenceSeparator != "" && sentenceSeparator != null)
                        {
                            buffer.Append(currentString.Substring(0, separatorOccurence + sentenceSeparator.Length));

                            var sentence = ParseSentence(buffer.ToString(), sentenceSeparator);

                            workedSentences.Add(sentence);

                            buffer.Clear();
                        }

                        currentString = currentString.Substring(separatorOccurence + sentenceSeparator.Length);

                        if (currentString.StartsWith(spaceSeparator))
                        {
                            currentString = currentString.Substring(spaceSeparator.Length);
                        }  
                    }
                }
            }

            Text textResult = new Text(workedSentences);

            return textResult;
        }

        private ISentence ParseSentence(string source, string sentenceSeparator)
        {
            var items = ParseSentenceItems(source, sentenceSeparator);

            Sentence sentence = new Sentence(items);

            return sentence;
        }

        private ICollection<ISentenceItem> ParseSentenceItems(string sentenceSource, string sentenceSeparator)
        {

            List<ISentenceItem> sentenceItems = new List<ISentenceItem>();

            int separatorOccurence = -1;

            List<string> itemParts = new List<string>();

            var wordSeparators = _symbolsContainer.WordSeparators().ToList();
            var allSeparators = _symbolsContainer.All().ToList();

            int bufferlength = 10000;

            StringBuilder buffer = new StringBuilder(bufferlength);

            while (sentenceSource.Length > 0)
            {
                var stringItems = sentenceSource.Split(wordSeparators[0], StringSplitOptions.RemoveEmptyEntries).ToList();

                foreach (var item in stringItems)
                {
                    separatorOccurence = -1;

                    foreach (var separator in allSeparators)
                    {
                        separatorOccurence = item.IndexOf(separator);

                        if (separatorOccurence >= 0 && separatorOccurence <= item.Length)
                        {
                            itemParts.Clear();

                            itemParts.Add(item.Substring(0, separatorOccurence));

                            itemParts.Add(separator);

                            itemParts.Add(item.Substring(++separatorOccurence));
                            break;
                        }
                    }

                    if (separatorOccurence >= 0 && separatorOccurence <= item.Length)
                    {
                        itemParts = itemParts.Where(ip => ip.Length > 0).Select(ip => ip).ToList();

                        itemParts.ForEach(ip => sentenceItems.Add(_sentenceItemFactory.Create(ip)));

                        sentenceItems.Add(_sentenceItemFactory.Create(wordSeparators[0]));
                    }
                    else
                    {
                        sentenceItems.Add(_sentenceItemFactory.Create(item));

                        sentenceItems.Add(_sentenceItemFactory.Create(wordSeparators[0]));
                    }

                    sentenceSource = sentenceSource.Substring(item.Length);

                    if (sentenceSource.StartsWith(wordSeparators[0]))
                    {
                        sentenceSource = sentenceSource.Substring(wordSeparators[0].Length);
                    }
                }
            }

            return sentenceItems;
        }

    }
}
