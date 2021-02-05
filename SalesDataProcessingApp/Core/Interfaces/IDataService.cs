using DAL.ModelsEntities;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IDataService
    {
        void ProcessFile(object filePath);
        void ProcessOrderEntity(OrderEntity orderEntity);
        void SplitClientNames(ref List<OrderEntity> orders);
    }
}