using System;
using System.Collections.Generic;
using System.Text;

namespace NewYearGift.Core.Services
{
    public static class TypeConversionService
    {
        public static int CheckAndConvertInputToInt(string input)
        {
            int resultInt;

            if (int.TryParse(input, out resultInt))
            {
                return resultInt;
            }
            else
            {
                resultInt = -1;
                return resultInt;
            }
        }

        public static List<int> CheckAndConvertInputArrayToInt(string[] inputArray)
        {
            int intItem;
            List<int> resultIntArray = new List<int>();

            foreach (string item in inputArray)
            {
                if (int.TryParse(item, out intItem))
                {
                    resultIntArray.Add(intItem);
                }
            }

            return resultIntArray;
        }
    }
}
