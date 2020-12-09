using JsonKnownTypes;
using NewYearGift.DAL.Models.Sweets.Parameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace NewYearGift.DAL.Models.Sweets
{
    public class AlcoholicSugarSweet : Sweet
    {
        public int SugarWeight { get; private set; }
        public int AlcoholDegree { get; private set; }
        
        public AlcoholicSugarSweet(string name, int weight, int kkal, Filling filling, Shape shape, int sugarWeight, int alcoholDegree)
            : base(name, weight, kkal, filling, shape)
        {
            this.SugarWeight = sugarWeight;
            this.AlcoholDegree = alcoholDegree;
        }
    }
}
