using TextObjectModel.App.Models;

namespace TextObjectModel.DAL.Repositories.Interfaces
{
    public interface IDataRepository
    {
        string GetDataPath();
        string GetModelPath();
        DataObjectModel ReadData();
        void SaveData(DataObjectModel data);
        DataObjectModel UpdateObjectModel(Text data);
    }
}
