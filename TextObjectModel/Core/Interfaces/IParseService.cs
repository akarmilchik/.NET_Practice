using TextObjectModel.App.Constants;

namespace TextObjectModel.Core.Interfaces
{
    public interface IParseService
    {
        string ClearSentenceStringLine(string stringLine, SymbolsContainer symbolsContainer);
        string FindSeparator(string currentString, string spaceSeparator, ref int separatorOccurence, SymbolsContainer symbolsContainer);
    }
}
