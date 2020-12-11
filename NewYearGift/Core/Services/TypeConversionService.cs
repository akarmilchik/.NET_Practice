using NewYearGift.Core.Services.Interfaces;
using System.Collections.Generic;

namespace NewYearGift.Core.Services
{
    public class TypeConversionService : ITypeConversionService
    {
        public int CheckAndConvertInputToInt(string input)
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

        public IEnumerable<int> CheckAndConvertInputArrayToInt(string[] inputArray)
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
