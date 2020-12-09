using JsonKnownTypes;
using Newtonsoft.Json;
using NewYearGift.DAL.Models.Sweets.Parameters;


namespace NewYearGift.DAL.Models.Sweets
{
    [JsonConverter(typeof(JsonKnownTypesConverter<Sweet>))]
    [JsonDiscriminator(Name = "ChildClass")]
    [JsonKnownType(typeof(SugarSweet))]
    [JsonKnownType(typeof(SugarFreeSweet))]
    [JsonKnownType(typeof(AlcoholicSweet))]
    [JsonKnownType(typeof(AlcoholicSugarSweet))]

    public abstract class Sweet
    {
        public string Name { get; private set; }
        public int Weight { get; private set; }
        public int Kkal { get; private set; }
        public Filling Filling { get; private set; }
        public Shape Shape { get; private set; }
        
        public Sweet(string name, int weight, int kkal, Filling filling, Shape shape)
        {
            this.Name = name;
            this.Weight = weight;
            this.Kkal = kkal;
            this.Filling = filling;
            this.Shape = shape;

        }
        
    }
}
