using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TextObjectModel.App.Constants;
using TextObjectModel.App.Interfaces;
using TextObjectModel.App.Models;
using TextObjectModel.DAL.Factories;
using TextObjectModel.DAL.Repositories;
using TextObjectModel.DAL.Repositories.Interfaces;

namespace TextObjectModel.Core.Services
{
    public class Parser
    {
        private SymbolsContainer _symbolsContainer;
        private ISentenceItemFactory _wordFactory;
        private ISentenceItemFactory _punctuationFactory;
        private IDataRepository _dataRepository;
        private SentenceItemFactory _sentenceItemFactory;
        private InternService _internService;

        public Parser(SymbolsContainer symbolsContainer, WordFactory wordFactory, PunctuationFactory punctuationFactory, DataRepository dataRepository, SentenceItemFactory sentenceItemFactory, InternService internService)
        {
            this._symbolsContainer = symbolsContainer;
            this._wordFactory = wordFactory;
            this._punctuationFactory = punctuationFactory;
            this._dataRepository = dataRepository;
            this._sentenceItemFactory = sentenceItemFactory;
            this._internService = internService;
        }

        public Text Parse(string dataPath)
        {
            _internService.InternSeparators(_symbolsContainer);

            var orderedSentenceSeparators = _symbolsContainer.SentenceSeparators().OrderByDescending(x => x.Length);

            var wordSeparators = _symbolsContainer.WordSeparators().ToList();

            int bufferlength = 10000;

            List<ISentence> workedSentences = new List<ISentence>();

            StringBuilder buffer = new StringBuilder(bufferlength);

            buffer.Clear();

            using (StreamReader reader = new StreamReader(dataPath, Encoding.Default))
            {
                string currentString;

                string sentenceSeparator = "";

                while ((currentString = reader.ReadLine()) != null)
                {
                    currentString = ClearSentenceStringLine(currentString);

                    while (currentString.Length > 0)
                    {
                        int separatorOccurence = -1;

                        int separatorOnlyOccurence = -1;

                        int separatorFollowedSpaceOccurence = -1;

                        foreach (var obj in orderedSentenceSeparators)
                        {
                            separatorOnlyOccurence = currentString.IndexOf(obj);

                            separatorFollowedSpaceOccurence = currentString.IndexOf(obj + wordSeparators[0]);

                            if (separatorOnlyOccurence > 0 && separatorOnlyOccurence < currentString.Length - 1)
                            {
                                separatorOnlyOccurence = CheckSeparatorOccurence(currentString, separatorOnlyOccurence, obj, wordSeparators);
                            }

                            if (separatorFollowedSpaceOccurence >= 0 && separatorFollowedSpaceOccurence < currentString.Length - 1)
                            {
                                sentenceSeparator = currentString.ElementAt(separatorFollowedSpaceOccurence).ToString();

                                separatorOccurence = separatorFollowedSpaceOccurence;

                                break;
                            }
                            else if (separatorOnlyOccurence >= 0 && separatorOnlyOccurence == (currentString.Length - 1))
                            {
                                sentenceSeparator = currentString.ElementAt(separatorOnlyOccurence).ToString();

                                separatorOccurence = separatorOnlyOccurence;

                                break;
                            }
                            
                        }

                        if (sentenceSeparator != "" && sentenceSeparator != null)
                        {
                            buffer.Append(currentString.Substring(0, separatorOccurence + sentenceSeparator.Length));

                            var sentence = this.ParseSentence(buffer.ToString(), sentenceSeparator);

                            workedSentences.Add(sentence);

                            buffer.Clear();
                        }
                        else
                        {
                            buffer.Append(" ");

                            buffer.Append(currentString);
                        }

                        currentString = currentString.Substring(separatorOccurence + sentenceSeparator.Length);

                        if (currentString.StartsWith(wordSeparators[0]))
                            currentString = currentString.Substring(wordSeparators[0].Length);

                    }
                }
            }

            Text textResult = new Text(workedSentences);

            return textResult;
        }

        protected string ClearSentenceStringLine(string stringLine)
        {
            var wordSeparators = _symbolsContainer.WordSeparators().ToList();

            foreach (string badSymbol in _symbolsContainer.BadSymbols())
            {
                stringLine = stringLine.Replace(badSymbol, wordSeparators[0]);
            }

            if (stringLine.StartsWith(wordSeparators[0]))
                stringLine = stringLine.Substring(wordSeparators[0].Length);

            return stringLine;    
        }

        protected int CheckSeparatorOccurence(string currentString, int separatorOccurence, string separator, List<string> wordSeparators)
        {
            var res = currentString.Substring(separatorOccurence, 2);

            while((separatorOccurence != (currentString.Length - 1)) && (res[1].ToString() != wordSeparators[0]))
            {
                var cutString = currentString.Substring(separatorOccurence + 1);
                
                separatorOccurence = cutString.IndexOf(separator) + separatorOccurence + 1;

                if(separatorOccurence < currentString.Length - 1)
                    res = currentString.Substring(separatorOccurence, 2);
            }
            
            return separatorOccurence;
        }
        
        protected ISentence ParseSentence(string source, string sentenceSeparator)
        {
            var items = ParseSentenceItems(source, sentenceSeparator);

            Sentence sentence = new Sentence(items);

            return sentence;
        }

        protected ICollection<ISentenceItem> ParseSentenceItems(string sentenceSource, string sentenceSeparator)
        { 

            List<ISentenceItem> sentenceItems = new List<ISentenceItem>();

            int separatorOccurence = -1;

            string wordSeparator = "";

            var wordSeparators = _punctuationContainer.WordSeparators().ToList();

            int bufferlength = 10000;

            StringBuilder buffer = new StringBuilder(bufferlength);

            while (sentenceSource.Length > 0)
            {

                var res = sentenceSource.Split(wordSeparators.ToArray(), StringSplitOptions.RemoveEmptyEntries);




                buffer.Clear();

                wordSeparator = "";

                separatorOccurence = -1;

                foreach (var obj in wordSeparators)
                {
                    var occurence = sentenceSource.IndexOf(obj);

                    if ((occurence != -1 && separatorOccurence > occurence) || separatorOccurence == -1)
                    {
                        separatorOccurence = occurence;
                    }

                }

                if (separatorOccurence >= 0)
                {
                    wordSeparator = sentenceSource.ElementAt(separatorOccurence).ToString();
                }

                if (wordSeparator != "" && wordSeparator != null)
                {
                    buffer.Append(sentenceSource.Substring(0, separatorOccurence));

                    if (buffer.Length > 0)
                    {
                        sentenceItems.Add(_sentenceItemFactory.Create(buffer.ToString()));
                    }

                    buffer.Clear();

                    buffer.Append(wordSeparator);

                    sentenceItems.Add(_sentenceItemFactory.Create(buffer.ToString()));

                    sentenceSource = sentenceSource.Substring(separatorOccurence + wordSeparator.Length);

                    if (sentenceSource.StartsWith(wordSeparators[0]))
                    {
                        buffer.Clear();

                        buffer.Append(wordSeparators[0]);

                        sentenceItems.Add(_sentenceItemFactory.Create(buffer.ToString()));

                        sentenceSource = sentenceSource.Substring(wordSeparators[0].Length);
                    }

                }
                else
                {
                    if (sentenceSource.Length > sentenceSeparator.Length)
                    {
                        sentenceSource = sentenceSource.Replace(sentenceSeparator, "");

                        buffer.Append(sentenceSource);

                        sentenceItems.Add(_sentenceItemFactory.Create(buffer.ToString()));

                        sentenceSource = sentenceSource.Replace(sentenceSource, "");

                    }
                    buffer.Clear();

                    buffer.Append(sentenceSeparator);

                    sentenceItems.Add(_sentenceItemFactory.Create(buffer.ToString()));

                    sentenceSource = sentenceSource.Replace(sentenceSeparator, "");
                }
            }

            return sentenceItems;
        }

        
    }
}
