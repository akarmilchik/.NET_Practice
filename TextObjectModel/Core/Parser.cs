using System;
using System.IO;
using System.Linq;
using System.Text;
using TextObjectModel.App.Constants;
using TextObjectModel.App.Interfaces;
using TextObjectModel.App.Models;
using TextObjectModel.DAL.Factories;

namespace TextObjectModel.Core.Services
{
    public class Parser
    {
        private SeparatorsContainer _separatorsContainer;
        private ISentenceItemFactory _wordFactory;
        private ISentenceItemFactory _punctuationFactory;

        public Parser(SeparatorsContainer separatorContainer)
        {
            this.SeparatorContainer = separatorContainer;
            this.WordFactory = new WordFactory();
            this.PunctuationFactory = new PunctuationFactory(this._separatorsContainer);
        }

        protected ISentenceItemFactory WordFactory
        {
            get { return _wordFactory; }
            set { _wordFactory = value; }
        }

        protected ISentenceItemFactory PunctuationFactory
        {
            get { return _punctuationFactory; }
            set { _punctuationFactory = value; }
        }

        protected SeparatorsContainer SeparatorContainer
        {
            get { return _separatorsContainer; }
            set { _separatorsContainer = value; }
        }

        public Text Parse(TextReader reader)
        {
            var orderedSentenceSeparators = SeparatorContainer.SentenceSeparators().OrderByDescending(x => x.Length);
            int bufferlength = 10000;
            Text textResult = new Text();
            StringBuilder buffer = new StringBuilder(bufferlength);

            buffer.Clear();
            string currentString = reader.ReadLine();
            while (currentString != null)
            {
                int firstSentenceSeparatorOccurence = -1;
                string firstSentenceSeparator = orderedSentenceSeparators.FirstOrDefault(
                    x =>
                    {
                        firstSentenceSeparatorOccurence = currentString.IndexOf(x);
                        return firstSentenceSeparatorOccurence >= 0;
                    });
                if (firstSentenceSeparator != null)
                {
                    buffer.Append(currentString.Substring(0, firstSentenceSeparatorOccurence + firstSentenceSeparator.Length));
                    textResult.sentences.Add(this.ParseSentence(buffer.ToString()));
                    buffer.Clear();
                    buffer.Append(currentString.Substring(firstSentenceSeparatorOccurence + firstSentenceSeparator.Length + 1, currentString.Length));
                }
                else
                {
                    buffer.Append(" ");
                    buffer.Append(currentString);
                }
                currentString = reader.ReadLine();
            }

            return textResult;
        }
        
        protected ISentence ParseSentence(string source)
        {

        }
    }
}
