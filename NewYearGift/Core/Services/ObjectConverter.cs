using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NewYearGift.DAL.Models.Sweets;
using System;

namespace NewYearGift.Core.Services
{
    class ObjectConverter : JsonConverter
    {
        static JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings() { ContractResolver = new BaseSpecifiedClassConverter() };

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Sweet));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);

            switch (jo["SweetTypeId"].Value<int>())
            {
                case 1:
                    return JsonConvert.DeserializeObject<Sweet>(jo.ToString(), SpecifiedSubclassConversion);
                case 2:
                    return JsonConvert.DeserializeObject<SugarSweet>(jo.ToString(), SpecifiedSubclassConversion);
                case 3:
                    return JsonConvert.DeserializeObject<AlcoholicSweet>(jo.ToString(), SpecifiedSubclassConversion);
                case 4:
                    return JsonConvert.DeserializeObject<AlcoholicSugarSweet>(jo.ToString(), SpecifiedSubclassConversion);
                default:
                    throw new Exception();
            }
            throw new NotImplementedException();
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

    }


}
