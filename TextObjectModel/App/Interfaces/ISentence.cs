using System.Collections.Generic;

namespace TextObjectModel.App.Interfaces
{
    public interface ISentence : IEnumerable<ISentenceItem>
    {
        void Add(ISentenceItem item);
        bool Remove(ISentenceItem item);
        int Count { get; }
    }
}
