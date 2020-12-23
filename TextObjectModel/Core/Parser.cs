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
        private PunctuationContainer _punctuationContainer;
        private ISentenceItemFactory _wordFactory;
        private ISentenceItemFactory _punctuationFactory;
        private IDataRepository _dataRepository;
        private SentenceItemFactory _sentenceItemFactory;

        public Parser(PunctuationContainer separatorContainer)
        {
            this.SeparatorContainer = separatorContainer;
            this.WordFactory = new WordFactory();
            this.PunctuationFactory = new PunctuationFactory(this._punctuationContainer);
            this.DataRepository = new DataRepository();
            this.SentenceItemFactory = new SentenceItemFactory(this._punctuationFactory, this._wordFactory);
        }

        protected ISentenceItemFactory WordFactory
        {
            get { return _wordFactory; }
            set { _wordFactory = value; }
        }

        public ISentenceItemFactory PunctuationFactory
        {
            get { return _punctuationFactory; }
            set { _punctuationFactory = value; }
        }

        protected PunctuationContainer SeparatorContainer
        {
            get { return _punctuationContainer; }
            set { _punctuationContainer = value; }
        }

        protected IDataRepository DataRepository
        {
            get { return _dataRepository; }
            set { _dataRepository = value; }
        }

        protected SentenceItemFactory SentenceItemFactory
        {
            get { return _sentenceItemFactory; }
            set { _sentenceItemFactory = value; }
        }

        public Text Parse()
        {
            var orderedSentenceSeparators = SeparatorContainer.SentenceSeparators().OrderByDescending(x => x.Length);

            var wordSeparators = SeparatorContainer.WordSeparators().ToList();

            int bufferlength = 10000;

            Text textResult = new Text();

            StringBuilder buffer = new StringBuilder(bufferlength);

            buffer.Clear();

            string path = DataRepository.GetDataPath();

            using (StreamReader reader = new StreamReader(path, Encoding.Default))
            {
                string currentString;

                string sentenceSeparator = "";

                while ((currentString = reader.ReadLine()) != null)
                {
                    int separatorOccurence = -1;

                    int separatorBehindSpaceOccurence = -1;

                    currentString = ClearSentenceStringLine(currentString);

                    foreach (var obj in orderedSentenceSeparators)
                    {
                        separatorOccurence = currentString.IndexOf(obj);

                        separatorBehindSpaceOccurence = currentString.IndexOf(obj + wordSeparators[0]);

                        if (separatorOccurence >= 0 && (separatorOccurence == (currentString.Length - 1) || separatorBehindSpaceOccurence >= 0))
                        {
                            sentenceSeparator = currentString.ElementAt(separatorOccurence).ToString();
                            break;
                        }
                    }

                    if (sentenceSeparator != "" && sentenceSeparator != null)
                    {
                        buffer.Append(currentString.Substring(0, separatorOccurence + sentenceSeparator.Length));

                        var sentence = this.ParseSentence(buffer.ToString());

                        textResult.sentences.Add(sentence);

                        buffer.Clear();

                        buffer.Append(currentString.Substring(separatorOccurence + sentenceSeparator.Length + 1, currentString.Length));
                    }
                    else
                    {
                        buffer.Append(" ");

                        buffer.Append(currentString);
                    }
                    currentString = reader.ReadLine();
                }
            }

            return textResult;
        }


        protected string ClearSentenceStringLine(string stringLine)
        {
            var wordSeparators = SeparatorContainer.WordSeparators().ToList();

            foreach (string badSymbol in SeparatorContainer.badSymbols)
            {
                stringLine = stringLine.Replace(badSymbol, wordSeparators[0]);
            }

            return stringLine;    
        }
        
        protected ISentence ParseSentence(string source)
        {
            var items = ParseSentenceItems(source);

            Sentence sentence = new Sentence(items);

            return sentence;
        }

        protected ICollection<ISentenceItem> ParseSentenceItems(string sentenceSource)
        {
            List<ISentenceItem> sentenceItems = new List<ISentenceItem>();

            int separatorOccurence = -1;

            string wordSeparator = "";

            var wordSeparators = SeparatorContainer.WordSeparators().ToList();

            int bufferlength = 10000;

            StringBuilder buffer = new StringBuilder(bufferlength);

            buffer.Clear();

            while (sentenceSource.Length > 0)
            {

                foreach (var obj in wordSeparators)
                {
                    separatorOccurence = sentenceSource.IndexOf(obj);

                    if (separatorOccurence >= 0)
                    {
                        wordSeparator = sentenceSource.ElementAt(separatorOccurence).ToString();
                        break;
                    }
                }

                if (wordSeparator != "" && wordSeparator != null)
                {
                    buffer.Append(sentenceSource.Substring(0, separatorOccurence));

                    sentenceItems.Add(SentenceItemFactory.Create(buffer.ToString()));

                    buffer.Clear();

                    buffer.Append(wordSeparator);

                    sentenceItems.Add(SentenceItemFactory.Create(buffer.ToString()));

                    sentenceSource = sentenceSource.Substring(separatorOccurence + wordSeparator.Length);
                }
                else
                {
                    buffer.Append(wordSeparators[0]);

                    buffer.Append(sentenceSource);
                }
            }

            return sentenceItems;
        }
    }
}
