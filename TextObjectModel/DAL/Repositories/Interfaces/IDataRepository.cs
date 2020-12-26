using TextObjectModel.App.Models;

namespace TextObjectModel.DAL.Repositories.Interfaces
{
    public interface IDataRepository
    {
        string GetDataPath();
        string GetModelPath();
        DataObjectModel ReadData();
        DataObjectModel UpdateObjectModel(Text data);
        void SaveData(DataObjectModel data);
    }
}
