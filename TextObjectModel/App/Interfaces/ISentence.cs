using System.Collections.Generic;

namespace TextObjectModel.App.Interfaces
{
    public interface ISentence : IEnumerable<ISentenceItem>
    {
        ICollection<ISentenceItem> items { get; }
        int Count { get; }
        void Add(ISentenceItem item);
        bool Remove(ISentenceItem item);
    }
}
