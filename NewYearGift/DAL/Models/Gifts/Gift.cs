using NewYearGift.App.Constants;
using NewYearGift.DAL.Models.Sweets;
using System.Collections.Generic;
using System.Linq;

namespace NewYearGift.Models.Gifts
{
    abstract class Gift
    {
        public int Id { get; set; }
        public int Weight { get; set; }
        public int Kkal { get; set; }
        public int CountOfSweets { get; set; }
        public List<Sweet> Sweets { get; set; }
        public Presentee BelongToPresentee { get; set; }

    }
}
