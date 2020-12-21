using System.Collections.Generic;
using System.Linq;

namespace TextObjectModel.App.Constants
{
    public class SeparatorsContainer
    {
        private string[] sentenceSeparators = new string[] { "?", "!", ".", "...", "?!" };
        private string[] wordSeparators = new string[] { " ", " - " };

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
