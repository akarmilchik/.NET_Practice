using DAL.ModelsEntities;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IParseService
    {
        List<OrderEntity> ReadCSVFile(string location, Serilog.Core.Logger logger);
    }
}