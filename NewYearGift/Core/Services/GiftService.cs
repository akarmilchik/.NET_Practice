using NewYearGift.App.Constants;
using NewYearGift.App.Interfaces;
using NewYearGift.App.Models.Sweets;
using NewYearGift.Core.Services.Interfaces;
using NewYearGift.Models.Gifts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewYearGift.Core.Services
{
    public class GiftService : IGiftService
    {
        public int CalculateGiftWeight(Gift gift)
        {
            return gift.Sweets.Sum(s => s.Weight);
        }

        public IEnumerable<Sweet> SortSweetBy(Func<Sweet, string> keySelector, IEnumerable<Sweet> sweets, SortOrder sortOrder)
        {
            return sortOrder == SortOrder.Ascending ? sweets.OrderBy(keySelector) : sweets.OrderByDescending(keySelector);
        }

        public IEnumerable<Sweet> SortSweetBy(Func<Sweet, int> keySelector, IEnumerable<Sweet> sweets, SortOrder sortOrder)
        {
            return sortOrder == SortOrder.Ascending ? sweets.OrderBy(keySelector) : sweets.OrderByDescending(keySelector);
        }

        public IEnumerable<Sweet> GetSweetsByPresentee(IEnumerable<Sweet> sweets, Presentee presentee) 
        {
            return sweets.Where(s => presentee == Presentee.Adult || (presentee == Presentee.Children && !(s is IAlcoholicSweet)));
        }

        public IEnumerable<Sweet> GetSweetsByIndexRange(IEnumerable<Sweet> sweets, IEnumerable<int> indexRange)
        {
            return sweets.Select((sweet, index) => new { Item = sweet, Index = index })
                 .Where(n => indexRange.Contains(n.Index + 1))
                 .Select(n => n.Item); ;
        }

        public IEnumerable<Sweet> GetSweetsByRange(Func<Sweet, int> keySelector, IEnumerable<Sweet> sweets, int firstRangeValue, int lastRangeValue)
        {
            return sweets.Where(s => keySelector(s) >= firstRangeValue && keySelector(s) <= lastRangeValue); ;        
        }

        public Gift MakeGift(ICollection<Sweet> sweets, Presentee presentee)
        {
            return new Gift()
            {
                Sweets = sweets,
                Weight = sweets.Sum(s => s.Weight),
                Kkal = sweets.Sum(s => s.Kkal),
                CountOfSweets = sweets.Count(),
                BelongToPresentee = presentee
            };
        }
    }
}
