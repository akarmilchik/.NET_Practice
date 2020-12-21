using TextObjectModel.App.Models;

namespace TextObjectModel.App.Interfaces
{
    public interface IPunctuation : ISentenceItem
    {
        Symbol Value { get; }
    }
}
