using Newtonsoft.Json;
using NewYearGift.Core.Services;
using NewYearGift.DAL.Models.Sweets.Parameters;


namespace NewYearGift.DAL.Models.Sweets
{
    [JsonConverter(typeof(ObjectConverter))]
    public abstract class Sweet
    {
        public string Name { get; private set; }
        public int Weight { get; private set; }
        public int Kkal { get; private set; }
        public Filling Filling { get; set; }
        public Shape Shape { get; set; }
        public int SweetTypeId { get; set; }
    }
}
