using NewYearGift.App.Interfaces;
using NewYearGift.App.Models.Sweets.Parameters;
using System;

namespace NewYearGift.App.Models.Sweets
{
    public class AlcoholicSweet : Sweet, IAlcoholicSweet
    {
        public int AlcoholDegree { get; private set; }

        public AlcoholicSweet(string name, int weight, int kkal, Filling filling, Shape shape, int alcoholDegree)
            : base(name, weight, kkal, filling, shape)
        {
            this.AlcoholDegree = alcoholDegree;
        }

        public override void PrintData()
        {
            Console.WriteLine($"    Name: {this.Name}");
            Console.WriteLine($"    Weight: {this.Weight.ToString()}");
            Console.WriteLine($"    Calorie: {this.Kkal.ToString()}");
            Console.WriteLine($"    {this.Filling.ToString()}");
            Console.WriteLine($"    {this.Shape.ToString()}");
            Console.WriteLine($"    Alcohol degree: {this.AlcoholDegree.ToString()}");
        }

        protected override string GetCaloriesBySugar(int sugarWeight)
        {
            return "0";
        }
    }
}
