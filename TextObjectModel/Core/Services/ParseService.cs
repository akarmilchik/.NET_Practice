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

            string needString = currentString;

            int resultOccurence = 0;

            partOfStringWithSeparator = getPartOfStringWithSeparator(needString, separatorOccurence, separator, partOfStringWithSeparator);

            while (separatorOccurence >= 0 && (separatorOccurence != (needString.Length - separator.Length)) && partOfStringWithSeparator.Last().ToString() != SymbolsContainer.Space)
            {
                var cutPreString = needString.Substring(0, separatorOccurence + separator.Length - 1);

                var cutString = needString.Substring(separatorOccurence + separator.Length);

                //if (partOfStringWithSeparator == separator)
                //{
                // separatorOccurence = CheckSeparatorContains(separator, separatorOccurence, partOfStringWithSeparator, cutPreString, cutString);
                // }

                var containsSeparatorinPreString = isStringContainsSeparator(cutPreString, separator);

                var containsSeparatorinAfterString = isStringContainsSeparator(cutString, separator);


                if (containsSeparatorinAfterString)
                {
                    resultOccurence += separatorOccurence + separator.Length;
                    separatorOccurence = cutString.IndexOf(separator);
                    partOfStringWithSeparator = getPartOfStringWithSeparator(cutString, separatorOccurence, separator, partOfStringWithSeparator);
                    needString = cutString;

                }
                else if (containsSeparatorinPreString)
                {
                    separatorOccurence = cutPreString.IndexOf(separator);
                    partOfStringWithSeparator = getPartOfStringWithSeparator(cutPreString, separatorOccurence, separator, partOfStringWithSeparator);
                    needString = cutPreString;
                    resultOccurence += separatorOccurence;
                }
                else
                {
                    return -1;
                }
            }

            resultOccurence += separatorOccurence;

            return resultOccurence;
        }

        private int CheckSeparatorContains(string separator, int separatorOccurence, string partWithSeparator, string cutPreString, string cutString)
        {/*
            var containsSeparatorinPreString = isStringContainsSeparator(cutPreString);

            var containsSeparatorinAfterString = isStringContainsSeparator(cutString);

            if (!containsSeparatorinPreString && !containsSeparatorinAfterString)
            {
                return separatorOccurence;
            }
            */
            return -1;
        }

        private bool isStringContainsSeparator(string partString, string separator)
        {
            string result = string.Empty;

            if (partString.IndexOf(separator + SymbolsContainer.Space) >= 0)
            {
                result = partString.Substring(partString.IndexOf(separator + SymbolsContainer.Space));
            }

            if (result == string.Empty)
            {
                //result = partString.Substring(partString.Length - separator.Length);
            }


            if ((partString.Contains(separator)) ||
                (partString.Contains(separator) && result == separator))
            {
                return true;
            }
            

            return false;
        }

        private bool isStringContainsSeparator(string partString)
        {
            string result = string.Empty;

            var separators = SymbolsContainer.SentenceSeparators().ToArray();

            for (int i = 0; i < separators.Length; i++)
            {
                result = string.Empty;

                if (partString.IndexOf(separators[i] + SymbolsContainer.Space) >= 0)
                {
                    result = partString.Substring(partString.IndexOf(separators[i] + SymbolsContainer.Space));
                }

                if (result == string.Empty)
                {
                    result = partString.Substring(partString.Length - separators[i].Length);
                }

                if ((partString.Contains(separators[i])) ||
                    (partString.Contains(separators[i]) && result == separators[i]))
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
