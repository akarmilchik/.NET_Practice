using NewYearGift.App.Constants;
using NewYearGift.App.Models.Sweets;
using System.Collections.Generic;

namespace NewYearGift.App.Interfaces
{
    public interface IGift
    {
        int Weight { get; set; }
        int Kkal { get; set; }
        int CountOfSweets { get; set; }
        IEnumerable<Sweet> Sweets { get; set; }
        Presentee BelongToPresentee { get; set; }
        void PrintData();
    }
}
