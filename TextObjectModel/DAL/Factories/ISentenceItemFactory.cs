using TextObjectModel.App.Interfaces;

namespace TextObjectModel.DAL.Factories
{
    public interface ISentenceItemFactory
    {
        ISentenceItem Create(string chars);
    }
}
