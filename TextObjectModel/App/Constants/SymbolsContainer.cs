using System.Collections.Generic;
using System.Linq;

namespace TextObjectModel.App.Constants
{
    public static class SymbolsContainer
    {
        public const string Space = " ";
        private static readonly IEnumerable<string> _sentenceSeparators = new string[] { "...", ".", "?", "!", "?!" };
        private static readonly IEnumerable<string> _wordSeparators = new string[] { " ", "-", ",", ":", ";", "(", ")", "[","]", "\""};
        private static readonly IEnumerable<string> _badSymbols = new string[] { "  ", "   ", "\t", " \t", "\t " };
        private static readonly IEnumerable<string> _consonantLetters = new string[] { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z" };

        public static IEnumerable<string> SentenceSeparators() => _sentenceSeparators;

        public static IEnumerable<string> WordSeparators() => _wordSeparators;

        public static IEnumerable<string> All() => _sentenceSeparators.Concat(WordSeparators());
      
        public static IEnumerable<string> BadSymbols() => _badSymbols;

        public static IEnumerable<string> ConsonantLetters() => _consonantLetters.Concat(ConsonantLettersToUpper());

        public static IEnumerable<string> ConsonantLettersToUpper() => _consonantLetters.Select(cl => cl.ToUpper());
    }
}
