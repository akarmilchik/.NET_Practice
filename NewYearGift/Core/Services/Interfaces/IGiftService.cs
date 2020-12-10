using NewYearGift.App.Constants;
using NewYearGift.DAL.Models.Sweets;
using NewYearGift.Models.Gifts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewYearGift.Core.Services.Interfaces
{
    public interface IGiftService
    {
        int CalculateGiftWeight(Gift gift);
        List<Sweet> SortSweetByString(Func<Sweet, string> keySelector, List<Sweet> sweets, SortOrder sortOrder);
        List<Sweet> SortSweetByInt(Func<Sweet, int> keySelector, List<Sweet> sweets, SortOrder sortOrder);
        List<Sweet> GetSweetsByIndexRange(List<Sweet> sweets, List<int> indexRange);
        List<Sweet> FindSweetsBySugarRange(List<Sweet> sweets, int firstRangeValue, int lastRangeValue);
        List<Sweet> GetSweetsByPresentee(List<Sweet> sweets, Presentee presentee);
        Gift MakeGift(List<Sweet> sweets, Presentee presentee);


    }
}
