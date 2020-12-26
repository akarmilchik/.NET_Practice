using System.Collections.Generic;

namespace TextObjectModel.Core.Services
{
    public static class TypeConversionService
    {
        public static int ToInt(this string input)
        {
            if (int.TryParse(input, out var resultInt))
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
