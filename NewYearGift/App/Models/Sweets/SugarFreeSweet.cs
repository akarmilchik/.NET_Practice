using NewYearGift.App.Models.Sweets.Parameters;
using System;

namespace NewYearGift.App.Models.Sweets
{
    class SugarFreeSweet : Sweet
    {
        public SugarFreeSweet(string name, int weight, int kkal, Filling filling, Shape shape)
            : base(name, weight, kkal, filling, shape)
        {

        }

        protected override int GetCaloriesBySugar(int sugarWeight)
        {
            return 0;
        }

        public override void PrintData()
        {
            Console.WriteLine($"    Name: {this.Name}");
            Console.WriteLine($"    Weight: {this.Weight.ToString()}");
            Console.WriteLine($"    Calorie: {this.Kkal.ToString()}");
            Console.WriteLine($"    {this.Filling.ToString()}");
            Console.WriteLine($"    {this.Shape.ToString()}");
        }
    }
}
