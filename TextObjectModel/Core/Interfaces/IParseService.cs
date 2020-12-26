using TextObjectModel.App.Constants;
using TextObjectModel.App.Models;
using TextObjectModel.DAL.Repositories.Interfaces;

namespace TextObjectModel.Core.Interfaces
{
    public interface IParseService
    {
        string ClearSentenceStringLine(string stringLine, SymbolsContainer symbolsContainer);
        string FindSeparator(string currentString, string spaceSeparator, ref int separatorOccurence, SymbolsContainer symbolsContainer);
        Text ParseData(Parser parser, IDataRepository dataRepository);
    }
}
