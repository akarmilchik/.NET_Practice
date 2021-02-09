using DAL.ModelsEntities;

namespace Core.Interfaces
{
    public interface IDataService
    {
        void ProcessFile(object filePath);
        void ProcessOrderEntity(OrderEntity orderEntity);
    }
}