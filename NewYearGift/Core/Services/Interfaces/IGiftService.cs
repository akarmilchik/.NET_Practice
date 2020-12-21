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
        IEnumerable<Sweet> SortSweetBy(Func<Sweet, string> keySelector, IEnumerable<Sweet> sweets, SortOrder sortOrder);
        IEnumerable<Sweet> SortSweetBy(Func<Sweet, int> keySelector, IEnumerable<Sweet> sweets, SortOrder sortOrder);
        IEnumerable<Sweet> GetSweetsByIndexRange(IEnumerable<Sweet> sweets, IEnumerable<int> indexRange);
        IEnumerable<Sweet> GetSweetsByPresentee(IEnumerable<Sweet> sweets, Presentee presentee);
        Gift MakeGift(ICollection<Sweet> sweets, Presentee presentee);
        IEnumerable<Sweet> GetSweetsByRange(Func<Sweet, int> keySelector, IEnumerable<Sweet> sweets, int firstRangeValue, int lastRangeValue);
    }
}
