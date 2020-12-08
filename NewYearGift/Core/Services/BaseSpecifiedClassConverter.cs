using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NewYearGift.DAL.Models.Sweets;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewYearGift.Core.Services
{
    public class BaseSpecifiedClassConverter : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (typeof(Sweet).IsAssignableFrom(objectType) && !objectType.IsAbstract)
                return null; // pretend TableSortRuleConvert is not specified (thus avoiding a stack overflow)
            return base.ResolveContractConverter(objectType);
        }
    }
}
