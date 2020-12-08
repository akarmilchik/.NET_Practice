using JsonKnownTypes;
using Newtonsoft.Json;
using NewYearGift.Core.Services;
using NewYearGift.DAL.Models.Sweets.Parameters;


namespace NewYearGift.DAL.Models.Sweets
{
    [JsonConverter(typeof(JsonKnownTypesConverter<Sweet>))]
    public abstract class Sweet
    {
        public int SweetTypeId { get; private set; }
        public string Name { get; private set; }
        public int Weight { get; private set; }
        public int Kkal { get; private set; }
        public Filling Filling { get; private set; }
        public Shape Shape { get; private set; }
        
        public Sweet(int sweetTypeId, string name, int weight, int kkal, Filling filling, Shape shape)
        {
            this.SweetTypeId = sweetTypeId;
            this.Name = name;
            this.Weight = weight;
            this.Kkal = kkal;
            this.Filling = filling;
            this.Shape = shape;

        }
        
    }
}
