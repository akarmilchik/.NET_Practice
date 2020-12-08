using NewYearGift.DAL.Models.Sweets;
using NewYearGift.DAL.Models.Sweets.Parameters;
using NewYearGift.Models.Gifts;
using System.Collections.Generic;

namespace NewYearGift.App.Data
{
    public class DataModel
    {
        public IEnumerable<FillingType> FillingTypes { get; set; }
        public IEnumerable<ShapeType> ShapeTypes { get; set; }
        public IEnumerable<Sweet> Sweets { get; set; }
        public Gift Gift { get; set; }
    }
}
