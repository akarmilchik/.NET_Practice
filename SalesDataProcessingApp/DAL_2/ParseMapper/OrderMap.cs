using CsvHelper.Configuration;
using DAL_2.ModelsEntities;

namespace DAL_2.ParseMapper
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
