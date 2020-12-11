using NewYearGift.App.Models;

namespace NewYearGift.DAL.Repositories.Interfaces
{
    public interface IJsonDataRepository
    {
        string GetDataPath();
        void SaveData(JsonDataModel data, string dataPath);
        JsonDataModel ReadData(string dataPath);
    }
}
