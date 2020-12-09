﻿using JsonKnownTypes;
using NewYearGift.DAL.Models.Sweets.Parameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace NewYearGift.DAL.Models.Sweets
{
    public class AlcoholicSweet : Sweet
    {
        public int AlcoholDegree { get; private set; }
        
        public AlcoholicSweet(string name, int weight, int kkal, Filling filling, Shape shape, int alcoholDegree)
            : base(name, weight, kkal, filling, shape)
        {
            this.AlcoholDegree = alcoholDegree;
        }
    }
}
