using JsonKnownTypes;
using Newtonsoft.Json;
using NewYearGift.App.Models.Sweets.Parameters;
using System;

namespace NewYearGift.App.Models.Sweets
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

        protected Sweet(string name, int weight, int kkal, Filling filling, Shape shape)
        {
            this.Name = name;
            this.Weight = weight;
            this.Kkal = kkal;
            this.Filling = filling;
            this.Shape = shape;
        }

        protected abstract int GetCaloriesBySugar(int sugarWeight);

        public virtual void PrintData()
        {
            Console.WriteLine($"    Name: {this.Name}");
            Console.WriteLine($"    Weight: {this.Weight}");
            Console.WriteLine($"    Calorie: {this.Kkal}");
            Console.WriteLine($"    {this.Filling}");
            Console.WriteLine($"    {this.Shape}");
        }
    }
}
