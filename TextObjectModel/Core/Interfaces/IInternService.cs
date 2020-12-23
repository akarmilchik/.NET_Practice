using TextObjectModel.App.Constants;

namespace TextObjectModel.Core.Interfaces
{
    public interface IInternService
    {
        void InternSeparators(PunctuationContainer PunctuationContainer);
        void InternString(string item);
    }
}
