using System.Collections.Generic;
using TextObjectModel.App.Interfaces;
using TextObjectModel.App.Models;

namespace TextObjectModel.Core.Interfaces
{
    public interface IMenuService
    {
        IEnumerable<ISentenceItem> FindSentenceItemsInInterrogativeSentences(Text data);
        Text RemoveSentenceItemsGivenLengthAndStartsConsonantLetter(Text data);
        ISentence ReplaceSentenceItemsGivenLengthBySubstring(Text data);
    }
}
