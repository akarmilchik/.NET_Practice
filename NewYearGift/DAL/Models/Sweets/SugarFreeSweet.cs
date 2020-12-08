using JsonKnownTypes;
using NewYearGift.DAL.Models.Sweets.Parameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace NewYearGift.DAL.Models.Sweets
{

    class SugarFreeSweet : Sweet
    {
        public SugarFreeSweet(int sweetTypeId, string name, int weight, int kkal, Filling filling, Shape shape)
            : base(sweetTypeId, name, weight, kkal, filling, shape)
        {

        }
    }
}
