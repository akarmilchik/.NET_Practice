using TextObjectModel.App.Constants;
using TextObjectModel.Core.Interfaces;

namespace TextObjectModel.Core.Services
{
    public class InternService : IInternService
    {
        public void InternSeparators(SymbolsContainer PunctuationContainer)
        {
            foreach (var obj in PunctuationContainer.All())
            {
                string.Intern(obj);
            }
        }

        public void InternString(string item)
        {
            var internedResult = string.IsInterned(item) ?? "not interned";

            if (internedResult == "not interned")
            {
                string.Intern(item);
            }
        }

    }
}
