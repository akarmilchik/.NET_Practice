using System.Collections.Generic;

namespace TextObjectModel.App.Interfaces
{
    public interface ISentence : IEnumerable<ISentenceItem>
    {
        ICollection<ISentenceItem> Items { get; }
        void Add(ISentenceItem item);
        bool Remove(ISentenceItem item);
    }
}
