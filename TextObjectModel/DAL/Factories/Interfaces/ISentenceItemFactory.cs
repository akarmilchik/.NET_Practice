using TextObjectModel.App.Interfaces;

namespace TextObjectModel.DAL.Factories.Interfaces
{
    public interface ISentenceItemFactory
    {
        ISentenceItem Create(string chars);
    }
}
