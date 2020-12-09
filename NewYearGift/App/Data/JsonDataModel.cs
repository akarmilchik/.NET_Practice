using NewYearGift.DAL.Models.Sweets;
using NewYearGift.Models.Gifts;
using System.Collections.Generic;

namespace NewYearGift.App.Data
{
    public class JsonDataModel
    {
        public List<Sweet> AllSweets { get; set; }
        public Gift Gift { get; set; }
    }
}
