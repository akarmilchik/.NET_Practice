using NewYearGift.App.Constants;
using NewYearGift.DAL.Models.Sweets;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewYearGift.App.Interfaces
{
    public interface IGift
    {
        int Weight { get; set; }
        int Kkal { get; set; }
        int CountOfSweets { get; set; }
        List<Sweet> Sweets { get; set; }
        Presentee BelongToPresentee { get; set; }

    }
}
