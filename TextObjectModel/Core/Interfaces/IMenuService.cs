using System.Collections.Generic;
using TextObjectModel.App.Interfaces;
using TextObjectModel.App.Models;

namespace TextObjectModel.Core.Interfaces
{
    public interface IMenuService
    {
        void FindWordsInInterrogativeSentences(Text data);
        void PrintSentencesItems(IEnumerable<ISentenceItem> items);
    }
}
