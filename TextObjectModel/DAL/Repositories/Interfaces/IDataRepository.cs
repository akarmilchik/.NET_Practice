using TextObjectModel.App.Models;

namespace TextObjectModel.DAL.Repositories.Interfaces
{
    public interface IDataRepository
    {
        string GetDataPath();
        DataObjectModel ReadData();
        void SaveData(Text data);
    }
}
