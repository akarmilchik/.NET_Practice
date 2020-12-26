using System.Collections.Generic;
using System.Linq;

namespace TextObjectModel.App.Constants
{
    public class SymbolsContainer
    {
        private static readonly IEnumerable<string> _sentenceSeparators = new string[] { ".", "?", "!", "?!", "..." };
        private static readonly IEnumerable<string> _wordSeparators = new string[] { " ", "-", ",", ":", ";", "(", ")", "[","]", "\""};
        private static readonly IEnumerable<string> _badSymbols = new string[] { "  ", "   ", "\t", " \t", "\t " };
        private static readonly IEnumerable<string> _consonantLetters = new string[] { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z" };

        public IEnumerable<string> SentenceSeparators() => _sentenceSeparators;

        public IEnumerable<string> WordSeparators() => _wordSeparators;

        public IEnumerable<string> All() => _sentenceSeparators.Concat(WordSeparators());
      
        public IEnumerable<string> BadSymbols() => _badSymbols;

        public IEnumerable<string> ConsonantLetters() => _consonantLetters.Concat(ConsonantLettersToUpper()).AsEnumerable();

        public IEnumerable<string> ConsonantLettersToUpper() => _consonantLetters.Select(cl => cl.ToUpper());
    }
}
