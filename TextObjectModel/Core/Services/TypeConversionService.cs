using System.Collections.Generic;
using TextObjectModel.Core.Interfaces;

namespace TextObjectModel.Core.Services
{
    class TypeConversionService : ITypeConversionService
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
