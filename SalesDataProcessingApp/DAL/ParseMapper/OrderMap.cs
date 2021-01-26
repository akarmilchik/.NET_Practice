using CsvHelper.Configuration;
using DAL.ModelsEntities;

namespace DAL.ParseMapper
{
    public sealed class OrderMap : ClassMap<OrderEntity>
    {
        public OrderMap()
        {
            Map(x => x.Date).Name("Date");

            Map(x => x.Client.FirstName).Name("Client");

            Map(x => x.Product.Name).Name("Product");

            Map(x => x.Product.Cost).Name("Cost");
        }
    }
}
