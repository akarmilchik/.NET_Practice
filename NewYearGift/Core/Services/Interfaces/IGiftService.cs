using NewYearGift.App.Constants;
using NewYearGift.App.Models.Sweets;
using NewYearGift.Models.Gifts;
using System;
using System.Collections.Generic;

namespace NewYearGift.Core.Services.Interfaces
{
    public interface IGiftService
    {
        int CalculateGiftWeight(Gift gift);
        IEnumerable<Sweet> SortSweetByString(Func<Sweet, string> keySelector, IEnumerable<Sweet> sweets, SortOrder sortOrder);
        IEnumerable<Sweet> SortSweetByInt(Func<Sweet, int> keySelector, IEnumerable<Sweet> sweets, SortOrder sortOrder);
        IEnumerable<Sweet> GetSweetsByIndexRange(IEnumerable<Sweet> sweets, IEnumerable<int> indexRange);
        IEnumerable<Sweet> GetSweetsBySugarRange(IEnumerable<Sweet> sweets, int firstRangeValue, int lastRangeValue);
        IEnumerable<Sweet> GetSweetsByAlcoholRange(IEnumerable<Sweet> sweets, int firstRangeValue, int lastRangeValue);
        IEnumerable<Sweet> GetSweetsByWeightRange(IEnumerable<Sweet> sweets, int firstRangeValue, int lastRangeValue);
        IEnumerable<Sweet> GetSweetsByKkalRange(IEnumerable<Sweet> sweets, int firstRangeValue, int lastRangeValue);
        IEnumerable<Sweet> GetSweetsByPresentee(IEnumerable<Sweet> sweets, Presentee presentee);
        Gift MakeGift(IEnumerable<Sweet> sweets, Presentee presentee);
    }
}
