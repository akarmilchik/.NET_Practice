using DAL.ModelsEntities;
using System.Collections.Generic;

namespace Core_2.Interfaces
{
    public interface IParseService
    {
        List<OrderEntity> ReadCSVFile(string location);
    }
}