using System;
using System.Collections.Generic;
using System.Text;

namespace TextObjectModel.Core.Interfaces
{
    public interface ITypeConversionService
    {
        IEnumerable<int> CheckAndConvertInputArrayToInt(string[] inputArray);
        int CheckAndConvertInputToInt(string input);
    }
}
