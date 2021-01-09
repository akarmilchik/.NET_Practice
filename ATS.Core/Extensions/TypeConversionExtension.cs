using System.Collections.Generic;

namespace ATS.Core.Extensions
{
    public static class TypeConversionExtension
    {
        public static int ToInt(this char inputChar)
        {
            if (int.TryParse(inputChar.ToString(), out var resultInt))
            {
                return resultInt;
            }

            resultInt = -1;
            return resultInt;
        }

        public static IEnumerable<int> ToInt(this string[] inputArray)
        {
            foreach (string item in inputArray)
            {
                if (int.TryParse(item, out var intItem))
                {
                    yield return intItem;
                }
            }
        }
    }
}