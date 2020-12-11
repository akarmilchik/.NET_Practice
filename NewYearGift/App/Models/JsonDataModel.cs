using NewYearGift.App.Models.Sweets;
using NewYearGift.Models.Gifts;
using System.Collections.Generic;

namespace NewYearGift.App.Models
{
    public class JsonDataModel
    {
        public IEnumerable<Sweet> AllSweets { get; set; }
        public Gift Gift { get; set; }
    }
}
