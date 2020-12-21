using NewYearGift.App.Models;

namespace NewYearGift.DAL.Repositories.Interfaces
{
    public interface IDataRepository
    {
        void SaveData(JsonDataModel data);
        JsonDataModel ReadData();
    }
}
