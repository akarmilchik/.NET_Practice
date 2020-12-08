using JsonKnownTypes;
using NewYearGift.App.Constants;
using NewYearGift.DAL.Models.Sweets.Parameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace NewYearGift.DAL.Models.Sweets
{ 
    public class SugarSweet : Sweet
    {
        public int SugarWeight { get; private set; }
        
        public SugarSweet(int sweetTypeId, string name, int weight, int kkal, Filling filling, Shape shape, int sugarWeight) : base(sweetTypeId, name, weight, kkal, filling, shape)
        {
            this.SugarWeight = sugarWeight;
        }
    }
}
