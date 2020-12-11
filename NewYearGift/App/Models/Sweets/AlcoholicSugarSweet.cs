using NewYearGift.App.Constants;
using NewYearGift.App.Interfaces;
using NewYearGift.App.Models.Sweets.Parameters;
using System;

namespace NewYearGift.App.Models.Sweets
{
    public class AlcoholicSugarSweet : Sweet, IAlcoholicSweet, ISugarSweet
    {
        public int SugarWeight { get; private set; }
        public int AlcoholDegree { get; private set; }
        
        public AlcoholicSugarSweet(string name, int weight, int kkal, Filling filling, Shape shape, int sugarWeight, int alcoholDegree)
            : base(name, weight, kkal, filling, shape)
        {
            this.SugarWeight = sugarWeight;
            this.AlcoholDegree = alcoholDegree;
        }

        public override void PrintData()
        {
            Console.WriteLine($"    Name: {this.Name}");
            Console.WriteLine($"    Weight: {this.Weight.ToString()}");
            Console.WriteLine($"    Calorie: {this.Kkal.ToString()}");
            Console.WriteLine($"    Calculated calorie: {GetCaloriesBySugar(this.SugarWeight)}");
            Console.WriteLine($"    {this.Filling.ToString()}");
            Console.WriteLine($"    {this.Shape.ToString()}");
            Console.WriteLine($"    Sugar weight: {this.SugarWeight.ToString()}");
            Console.WriteLine($"    Alcohol degree: {this.AlcoholDegree.ToString()}");
        }

        protected override string GetCaloriesBySugar(int sugarWeight)
        {
            return (sugarWeight * ConstantValues.CaloriesPerSugarGram + sugarWeight * ConstantValues.CaloriesPerAlcohol).ToString();
        }
    }
}
