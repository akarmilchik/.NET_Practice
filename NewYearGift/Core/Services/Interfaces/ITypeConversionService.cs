using System;
using System.Collections.Generic;
using System.Text;

namespace NewYearGift.Core.Services.Interfaces
{
    public interface ITypeConversionService
    {
        int CheckAndConvertInputToInt(string input);
        List<int> CheckAndConvertInputArrayToInt(string[] inputArray);
    }
}
