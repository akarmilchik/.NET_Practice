using CsvHelper.Configuration;
using DAL.ModelsEntities;

namespace DAL.ParseMapper
{
    public sealed class OrderMap : ClassMap<OrderEntity>
    {
        public OrderMap()
        {
            Map(x => x.Date).TypeConverterOption.Format("dd-MM-yyyy HH:mm:ss");

            Map(x => x.Client.FirstName).Name("Client");

            Map(x => x.Product.Name).Name("Product");

            Map(x => x.Product.Cost).Name("Cost");
        }
    }
}
