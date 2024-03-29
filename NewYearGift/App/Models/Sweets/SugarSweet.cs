﻿using NewYearGift.App.Constants;
using NewYearGift.App.Interfaces;
using NewYearGift.App.Models.Sweets.Parameters;
using System;

namespace NewYearGift.App.Models.Sweets
{
    public class SugarSweet : Sweet, ISugarSweet
    {
        public int SugarWeight { get; private set; }

        public SugarSweet(string name, int weight, int kkal, Filling filling, Shape shape, int sugarWeight) : base(name, weight, kkal, filling, shape)
        {
            this.SugarWeight = sugarWeight;
        }

        protected override int GetCaloriesBySugar(int sugarWeight)
        {
            return sugarWeight * ConstantValues.CaloriesPerSugarGram;
        }

        public override void PrintData()
        {
            Console.WriteLine($"    Name: {this.Name}");
            Console.WriteLine($"    Weight: {this.Weight}");
            Console.WriteLine($"    Calorie: {this.Kkal}");
            Console.WriteLine($"    Calculated calorie: {GetCaloriesBySugar(this.SugarWeight)}");
            Console.WriteLine($"    {this.Filling}");
            Console.WriteLine($"    {this.Shape}");
            Console.WriteLine($"    Sugar weight: {this.SugarWeight}");
        }
    }
}
