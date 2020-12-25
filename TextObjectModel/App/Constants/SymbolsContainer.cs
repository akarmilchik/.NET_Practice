using System;
using System.Collections.Generic;
using System.Linq;

namespace TextObjectModel.App.Constants
{
    public class SymbolsContainer
    {
        private static readonly string[] _sentenceSeparators = new string[] { "?", "!", ".", "...", "?!" };
        private static readonly string[] _wordSeparators = new string[] { " ", "-", ",", ":", ";", "(", ")", "[","]", "\""};
        public static readonly string[] _badSymbols = new string[] { "  ", "\t" };
        public static readonly string[] _consonantLetters = new string[] { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z" };

        public IEnumerable<string> SentenceSeparators() => _sentenceSeparators.AsEnumerable();

        public IEnumerable<string> WordSeparators() => _wordSeparators.AsEnumerable();

        public IEnumerable<string> All() => _sentenceSeparators.Concat(WordSeparators());
      
        public IEnumerable<string> BadSymbols() => _badSymbols.AsEnumerable();

        public IEnumerable<string> ConsonantLetters() => _consonantLetters.Concat(ConsonantLettersToUpper()).AsEnumerable();

        public IEnumerable<string> ConsonantLettersToUpper()
        {
            return _consonantLetters.Select(cl => cl.ToUpper()).ToArray();
        }
    }
}
