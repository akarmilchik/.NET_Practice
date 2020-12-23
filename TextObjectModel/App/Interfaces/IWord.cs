using System.Collections.Generic;
using TextObjectModel.App.Models;

namespace TextObjectModel.App.Interfaces
{
    public interface IWord : ISentenceItem, IEnumerable<Symbol>
    {
        //Symbol this[int index] { get; }
        ICollection<Symbol> Symbols { get; }
        int Length { get; }
    }
}