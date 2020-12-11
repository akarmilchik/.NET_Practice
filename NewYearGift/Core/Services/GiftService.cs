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
            return sortOrder == SortOrder.Ascending ? sweets.OrderBy(keySelector).ToList() : sweets.OrderByDescending(keySelector).ToList();
        }

        public IEnumerable<Sweet> GetSweetsByPresentee(IEnumerable<Sweet> sweets, Presentee presentee)  //????????
        {
            return sweets.Where(s => ((presentee == Presentee.Children) && !(s is IAlcoholicSweet)));
        }

        public IEnumerable<Sweet> GetSweetsByIndexRange(IEnumerable<Sweet> sweets, IEnumerable<int> indexRange)
        {
            List<Sweet> resultSweets = new List<Sweet>();

            foreach (int index in indexRange)
            {
                if (sweets.ElementAt(index) != null)
                {
                    resultSweets.Add(sweets.ElementAt(index - 1));
                }
            }

            return resultSweets;
        }

        /*  ??????????
        public IEnumerable<Sweet> GetSweetsByParameterRange(Func<Sweet, string> keySelector, IEnumerable<Sweet> sweets, int firstRangeValue, int lastRangeValue)
        {
            return 
        }*/

        public IEnumerable<Sweet> GetSweetsBySugarRange(IEnumerable<Sweet> sweets, int firstRangeValue, int lastRangeValue)
        {
            List<SugarSweet> sugarSweets = new List<SugarSweet>();

            List<AlcoholicSugarSweet> alcoholSugarSweets = new List<AlcoholicSugarSweet>();

            List<Sweet> sweetsResult = new List<Sweet>();

            foreach (Sweet sweet in sweets)
            {
                if (sweet is SugarSweet)
                {
                    sugarSweets.Add(sweet as SugarSweet);
                }
                else if (sweet is AlcoholicSugarSweet)
                {
                    alcoholSugarSweets.Add(sweet as AlcoholicSugarSweet);
                }
            }

            sugarSweets = sugarSweets.Where(s => s.SugarWeight >= firstRangeValue && s.SugarWeight <= lastRangeValue).Select(s => s).ToList();

            alcoholSugarSweets = alcoholSugarSweets.Where(s => s.SugarWeight >= firstRangeValue && s.SugarWeight <= lastRangeValue).Select(s => s).ToList();

            foreach (SugarSweet sugarSweet in sugarSweets)
            {
                sweetsResult.Add(sugarSweet as Sweet);
            }

            foreach (AlcoholicSugarSweet alcoholSugarSweet in alcoholSugarSweets)
            {
                sweetsResult.Add(alcoholSugarSweet as Sweet);
            }

            return sweetsResult;
        }

        public IEnumerable<Sweet> GetSweetsByAlcoholRange(IEnumerable<Sweet> sweets, int firstRangeValue, int lastRangeValue)
        {
            List<AlcoholicSweet> alcoholSweets = new List<AlcoholicSweet>();

            List<AlcoholicSugarSweet> alcoholSugarSweets = new List<AlcoholicSugarSweet>();

            List<Sweet> sweetsResult = new List<Sweet>();

            foreach (Sweet sweet in sweets)
            {
                if (sweet is AlcoholicSweet)
                {
                    alcoholSweets.Add(sweet as AlcoholicSweet);
                }
                else if (sweet is AlcoholicSugarSweet)
                {
                    alcoholSugarSweets.Add(sweet as AlcoholicSugarSweet);
                }
            }

            alcoholSweets = alcoholSweets.Where(s => s.AlcoholDegree >= firstRangeValue && s.AlcoholDegree <= lastRangeValue).Select(s => s).ToList();

            alcoholSugarSweets = alcoholSugarSweets.Where(s => s.AlcoholDegree >= firstRangeValue && s.AlcoholDegree <= lastRangeValue).Select(s => s).ToList();

            foreach (AlcoholicSweet alcoholSweet in alcoholSweets)
            {
                sweetsResult.Add(alcoholSweet);
            }

            foreach (AlcoholicSugarSweet alcoholSweet in alcoholSugarSweets)
            {
                sweetsResult.Add(alcoholSweet);
            }

            return sweetsResult;
        }

        public IEnumerable<Sweet> GetSweetsByWeightRange(IEnumerable<Sweet> sweets, int firstRangeValue, int lastRangeValue)
        {
            sweets = sweets.Where(s => s.Weight >= firstRangeValue && s.Weight <= lastRangeValue).Select(s => s).ToList();

            return sweets;
        }

        public IEnumerable<Sweet> GetSweetsByKkalRange(IEnumerable<Sweet> sweets, int firstRangeValue, int lastRangeValue)
        {
            sweets = sweets.Where(s => s.Kkal >= firstRangeValue && s.Kkal <= lastRangeValue).Select(s => s).ToList();

            return sweets;
        }

        public Gift MakeGift(IEnumerable<Sweet> sweets, Presentee presentee)
        {
            Gift makedGift = new Gift() { };

            makedGift.Sweets = sweets;

            sweets.ToList().ForEach(s => makedGift.Weight += s.Weight);

            sweets.ToList().ForEach(s => makedGift.Kkal += s.Kkal);

            makedGift.CountOfSweets = sweets.Count();

            makedGift.BelongToPresentee = presentee;

            return makedGift;
        }
    }
}
