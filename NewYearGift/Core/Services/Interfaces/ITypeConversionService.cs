using System.Collections.Generic;

namespace NewYearGift.Core.Services.Interfaces
{
    public interface ITypeConversionService
    {
        int CheckAndConvertInputToInt(string input);
        IEnumerable<int> CheckAndConvertInputArrayToInt(string[] inputArray);
    }
}
