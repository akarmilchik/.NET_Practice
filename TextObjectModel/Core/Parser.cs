﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TextObjectModel.App.Constants;
using TextObjectModel.App.Interfaces;
using TextObjectModel.App.Models;
using TextObjectModel.Core.Interfaces;
using TextObjectModel.DAL.Factories;
using TextObjectModel.DAL.Factories.Interfaces;

namespace TextObjectModel.Core
{
    public class Parser
    {
        private readonly ISentenceItemFactory _sentenceItemFactory;
        private readonly IParseService _parseService;

        public Parser(ISentenceItemFactory sentenceItemFactory, IParseService parseService)
        {
            _sentenceItemFactory = sentenceItemFactory;
            _parseService = parseService;
        }

        public Text Parse(string dataPath)
        {
            int separatorOccurence = -1;

            List<ISentence> workedSentences = new List<ISentence>();

            try
            {
                using (StreamReader reader = new StreamReader(dataPath, Encoding.Default))
                {
                    string currentString;

                    while ((currentString = reader.ReadLine()) != null)
                    {
                        currentString = _parseService.ClearSentenceStringLine(currentString);

                        while (currentString.Length > 0)
                        {
                            string sentenceSeparator = _parseService.FindSeparator(currentString, ref separatorOccurence);

                            if (sentenceSeparator != string.Empty && !string.IsNullOrEmpty(sentenceSeparator))
                            {
                                var sentenceString = currentString.Substring(0, separatorOccurence + sentenceSeparator.Length);

                                var sentence = ParseSentence(sentenceString);

                                workedSentences.Add(sentence);
                            }

                            if (separatorOccurence >= 0)
                            {
                                currentString = currentString.Substring(separatorOccurence + sentenceSeparator.Length);
                            }

                            currentString = currentString.TrimStart();
                        }
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"File not found {e.Message}");
            }

            Text textResult = new Text(workedSentences);

            return textResult;
        }

        private ISentence ParseSentence(string source)
        {
            var items = ParseSentenceItems(source);

            Sentence sentence = new Sentence(items);

            return sentence;
        }

        private ICollection<ISentenceItem> ParseSentenceItems(string sentenceSource)
        {
            List<ISentenceItem> sentenceItems = new List<ISentenceItem>();

            int separatorOccurence = -1;

            List<string> itemParts = new List<string>();

            var allSeparators = SymbolsContainer.All().ToList();

            while (sentenceSource.Length > 0)
            {
                var stringItems = sentenceSource.Split(SymbolsContainer.Space, StringSplitOptions.RemoveEmptyEntries).ToList();

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

                            itemParts.Add(item.Substring(separatorOccurence + separator.Length));
                            break;
                        }
                    }

                    if (separatorOccurence >= 0 && separatorOccurence <= item.Length)
                    {
                        itemParts = itemParts.Where(ip => ip.Length > 0).Select(ip => ip).ToList();

                        itemParts.ForEach(ip => sentenceItems.Add(_sentenceItemFactory.Create(ip)));

                        sentenceItems.Add(_sentenceItemFactory.Create(SymbolsContainer.Space));
                    }
                    else
                    {
                        sentenceItems.Add(_sentenceItemFactory.Create(item));

                        sentenceItems.Add(_sentenceItemFactory.Create(SymbolsContainer.Space));
                    }

                    sentenceSource = sentenceSource.Substring(item.Length).TrimStart();
                }
            }

            return sentenceItems;
        }
    }
}
