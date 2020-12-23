using System.Collections.Generic;
using System.Linq;

namespace TextObjectModel.App.Constants
{
    public class PunctuationContainer
    {
        private string[] sentenceSeparators = new string[] { "?", "!", ".", "...", "?!" };
        private string[] wordSeparators = new string[] { " ", "-", ",", ":", ";", "(", ")", "[","]", "\""};
        public string[] badSymbols = new string[] { "  ", "\t" };

        public IEnumerable<string> SentenceSeparators()
        {
            return sentenceSeparators.AsEnumerable();
        }

        public IEnumerable<string> WordSeparators()
        {
            return wordSeparators.AsEnumerable();
        }

        public IEnumerable<string> All()
        {
            return sentenceSeparators.Concat(WordSeparators());
        }
    }
}
