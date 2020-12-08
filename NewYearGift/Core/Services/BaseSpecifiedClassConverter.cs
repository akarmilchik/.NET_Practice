using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NewYearGift.DAL.Models.Sweets;
using System;

namespace NewYearGift.Core.Services
{
    public class BaseSpecifiedClassConverter : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (typeof(Sweet).IsAssignableFrom(objectType) && !objectType.IsAbstract)
            {
                return null;
            }

            return base.ResolveContractConverter(objectType);
        }
    }
}
