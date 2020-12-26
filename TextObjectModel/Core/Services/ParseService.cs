using System.Linq;
using TextObjectModel.App.Constants;
using TextObjectModel.App.Models;
using TextObjectModel.Core.Interfaces;
using TextObjectModel.DAL.Repositories.Interfaces;

namespace TextObjectModel.Core.Services
{
    public class ParseService : IParseService
    {
        protected int CheckSeparatorOccurence(string currentString, int separatorOccurence, string separator, string spaceSeparator)
        {
            string partOfStringWithSeparator = "";

            partOfStringWithSeparator = FindSeparatorPosition(currentString, separatorOccurence, separator, partOfStringWithSeparator);

            while ((separatorOccurence != (currentString.Length - separator.Length)) && (partOfStringWithSeparator[separator.Length].ToString() != spaceSeparator))
            {
                var cutString = currentString.Substring(separatorOccurence + separator.Length);

                if (!(cutString.Contains(separator)))
                {
                    return -1;
                }

                separatorOccurence = cutString.IndexOf(separator) + separatorOccurence + 1;

                partOfStringWithSeparator = FindSeparatorPosition(currentString, separatorOccurence, separator, partOfStringWithSeparator);
            }

            return separatorOccurence;
        }

        protected string FindSeparatorPosition(string cutString, int separatorOccurence, string separator, string partOfStringWithSeparator)
        {
            if (separatorOccurence < cutString.Length - separator.Length)
            {
                partOfStringWithSeparator = cutString.Substring(separatorOccurence, separator.Length + 1);
            }
            else if (separatorOccurence == cutString.Length - separator.Length)
            {
                partOfStringWithSeparator = cutString.Substring(separatorOccurence, separator.Length);
            }

            return partOfStringWithSeparator;
        }

        public Text ParseData(Parser parser, IDataRepository dataRepository)
        {
            var path = dataRepository.GetDataPath();
 
            return parser.Parse(path);
        }

        public string FindSeparator(string currentString, string spaceSeparator, ref int separatorOccurence, SymbolsContainer symbolsContainer)
        {
            separatorOccurence = -1;

            int separatorOnlyOccurence = -1;

            int separatorFollowedSpaceOccurence = -1;

            string sentenceSeparator = "";

            var sentenceSeparators = symbolsContainer.SentenceSeparators();

            foreach (var separator in sentenceSeparators)
            {
                separatorOnlyOccurence = currentString.IndexOf(separator);

                separatorFollowedSpaceOccurence = currentString.IndexOf(separator + spaceSeparator);

                if (separatorOnlyOccurence > 0 && separatorOnlyOccurence <= currentString.Length - separator.Length)
                {
                    separatorOnlyOccurence = CheckSeparatorOccurence(currentString, separatorOnlyOccurence, separator, spaceSeparator);
                }

                if (separatorFollowedSpaceOccurence >= 0 && separatorFollowedSpaceOccurence < currentString.Length - separator.Length)
                {
                    sentenceSeparator = currentString.ElementAt(separatorFollowedSpaceOccurence).ToString();

                    separatorOccurence = separatorFollowedSpaceOccurence;

                    break;
                }
                else if (separatorOnlyOccurence >= 0 && separatorOnlyOccurence == (currentString.Length - separator.Length))
                {
                    sentenceSeparator = currentString.Substring(separatorOnlyOccurence, separator.Length).ToString();

                    separatorOccurence = separatorOnlyOccurence;

                    break;
                }
            }

            return sentenceSeparator;
        }

        public string ClearSentenceStringLine(string stringLine, SymbolsContainer symbolsContainer)
        {
            var wordSeparators = symbolsContainer.WordSeparators().ToList();

            foreach (string badSymbol in symbolsContainer.BadSymbols())
            {
                stringLine = stringLine.Replace(badSymbol, wordSeparators[0]);
            }

            if (stringLine.StartsWith(wordSeparators[0]))
            {
                stringLine = stringLine.Substring(wordSeparators[0].Length);
            }

            return stringLine;
        }
    }
}
