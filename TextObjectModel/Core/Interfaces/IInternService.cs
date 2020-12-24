using TextObjectModel.App.Constants;

namespace TextObjectModel.Core.Interfaces
{
    public interface IInternService
    {
        void InternSeparators(SymbolsContainer PunctuationContainer);
        void InternString(string item);
    }
}
