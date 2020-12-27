using System.Collections.Generic;
using System.Linq;
using TextObjectModel.App.Constants;
using TextObjectModel.Core.Interfaces;

namespace TextObjectModel.Core.Services
{
    public class ParseService : IParseService
    {
        public string FindSeparator(string currentString, ref int separatorOccurence)
        {
            separatorOccurence = -1;

            int separatorOnlyOccurence = -1;

            int separatorFollowedSpaceOccurence = -1;

            string sentenceSeparator = string.Empty;

            foreach (var separator in SymbolsContainer.SentenceSeparators().ToList().Select(s => s + SymbolsContainer.Space))
            {
                separatorFollowedSpaceOccurence = currentString.IndexOf(separator);

                if (separatorFollowedSpaceOccurence < 0)
                {
                    separatorOnlyOccurence = currentString.IndexOf(separator.TrimEnd());
                }    

                /*
                separatorFollowedSpaceOccurence = currentString.IndexOf(separator + SymbolsContainer.Space);
                */
                if (separatorOnlyOccurence > 0 && separatorOnlyOccurence <= currentString.Length - separator.TrimEnd().Length)
                {
                    separatorOnlyOccurence = CheckSeparatorOccurence(currentString, separatorOnlyOccurence, separator.TrimEnd());
                }
                

                if (separatorFollowedSpaceOccurence >= 0 && separatorFollowedSpaceOccurence < currentString.Length - separator.Length)
                {
                    sentenceSeparator = currentString.ElementAt(separatorFollowedSpaceOccurence).ToString();

                    separatorOccurence = separatorFollowedSpaceOccurence;

                    break;
                }
                if (separatorOnlyOccurence >= 0 && separatorOnlyOccurence == (currentString.Length - separator.TrimEnd().Length))
                {
                    sentenceSeparator = currentString.Substring(separatorOnlyOccurence, separator.TrimEnd().Length).ToString();

                    separatorOccurence = separatorOnlyOccurence;

                    break;
                }
            }

            return sentenceSeparator;
        }

        public string ClearSentenceStringLine(string stringLine)
        {
            foreach (var badSymbol in SymbolsContainer.BadSymbols())
            {
                stringLine = stringLine.Replace(badSymbol, SymbolsContainer.Space);
            }

            stringLine = stringLine.TrimStart();
            
            return stringLine;
        }

        private int CheckSeparatorOccurence(string currentString, int separatorOccurence, string separator)
        {
            string partOfStringWithSeparator = string.Empty;

            partOfStringWithSeparator = getPartOfStringWithSeparator(currentString, separatorOccurence, separator, partOfStringWithSeparator);

            while (separatorOccurence >= 0 && (separatorOccurence != (currentString.Length - separator.Length)) && partOfStringWithSeparator.Last().ToString() != SymbolsContainer.Space)
            {
                var cutPreString = currentString.Substring(0, separatorOccurence + separator.Length - 1);

                var cutString = currentString.Substring(separatorOccurence + separator.Length);

                //if (partOfStringWithSeparator == separator)
                //{
                    separatorOccurence = CheckSeparatorContains(separator, separatorOccurence, partOfStringWithSeparator, cutPreString, cutString);
               // }

                if (cutString.Length > separator.Length)
                {
                    separatorOccurence = cutString.IndexOf(separator) + separatorOccurence + 1;
                }
                else
                {
                    separatorOccurence = cutPreString.IndexOf(separator) + separatorOccurence + 1;
                }

                partOfStringWithSeparator = getPartOfStringWithSeparator(currentString, separatorOccurence, separator, partOfStringWithSeparator);
            }

            return separatorOccurence;
        }

        private int CheckSeparatorContains(string separator, int separatorOccurence, string partWithSeparator, string cutPreString, string cutString)
        {
            var containsSeparatorinPreString = isStringContainsSeparator(cutPreString);

            var containsSeparatorinAfterString = isStringContainsSeparator(cutString);

            if ((containsSeparatorinPreString && !containsSeparatorinAfterString) ||
                (!containsSeparatorinPreString && containsSeparatorinAfterString) ||
                (!containsSeparatorinPreString && !containsSeparatorinAfterString) )
            {
                return -1;
            }

            return separatorOccurence;
        }

        private bool isStringContainsSeparator(string partString)
        {
            var separatorsWithSpace = SymbolsContainer.SentenceSeparators().Select(s => s + SymbolsContainer.Space).ToList();

            for (int i = 0; i < separatorsWithSpace.Count; i++)
            {
                //var lastPartOfString = partString.Substring(partString.IndexOf(separatorsWithSpace[i].TrimEnd()));

                if ((partString.Contains(separatorsWithSpace[i])) ||
                    (partString.Contains(separatorsWithSpace[i].TrimEnd()) && partString.Substring(partString.IndexOf(separatorsWithSpace[i].TrimEnd())) == separatorsWithSpace[i].TrimEnd()))
                {
                    return true;
                }
            }

            return false;
        }

        private string getPartOfStringWithSeparator(string cutString, int separatorOccurence, string separator, string partOfStringWithSeparator)
        {
            if (separatorOccurence < 0)
            {
                return string.Empty;
            }

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
    }
}
