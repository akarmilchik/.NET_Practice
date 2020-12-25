using System.Collections.Generic;
using TextObjectModel.App.Interfaces;
using TextObjectModel.App.Models;

namespace TextObjectModel.Core.Interfaces
{
    public interface IMenuService
    {
        IEnumerable<ISentenceItem> FindWordsInInterrogativeSentences(Text data);
        Text RemoveWordsGivenLengthAndStartsConsonantLetter(Text data);
        Text ReplaceWordsGivenLengthBySubstring(Text data);
    }
}
