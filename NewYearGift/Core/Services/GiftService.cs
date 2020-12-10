using NewYearGift.App.Constants;
using NewYearGift.Core.Services.Interfaces;
using NewYearGift.DAL.Models.Interfaces;
using NewYearGift.DAL.Models.Sweets;
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
            int weight = 0;

            gift.Sweets.ForEach(s => weight += s.Weight);
            //gift.Sweets.Sum();

            return weight;
        }

        public List<Sweet> SortSweetByString(Func<Sweet, string> keySelector, List<Sweet> sweets, SortOrder sortOrder)
        {
            return sortOrder == SortOrder.Ascending ? sweets.OrderBy(keySelector).ToList() : sweets.OrderByDescending(keySelector).ToList();
        }

        public List<Sweet> SortSweetByInt(Func<Sweet, int> keySelector, List<Sweet> sweets, SortOrder sortOrder)
        {
            return sortOrder == SortOrder.Ascending ? sweets.OrderBy(keySelector).ToList() : sweets.OrderByDescending(keySelector).ToList();
        }

        public List<Sweet> GetSweetsByPresentee(List<Sweet> sweets, Presentee presentee)
        {
            return sweets.Where(s => !((presentee == Presentee.Children) && !(s is IAlcoholicSweet))).ToList();
        }

        public List<Sweet> GetSweetsByIndexRange(List<Sweet> sweets, List<int> indexRange)
        {
            List<Sweet> resultSweets = new List<Sweet>();

            foreach (int index in indexRange)
            {
                resultSweets.Add(sweets.ElementAt(index - 1));
            }

            return resultSweets;
        }

        public List<Sweet> GetSweetsByParameterRange(Func<Sweet, bool> keySelector, List<Sweet> sweets, int firstRangeValue, int lastRangeValue)
        {
            return sweets.Where(s => sweets. >= firstRangeValue && s.Equals());
        }


        public List<Sweet> GetSweetsBySugarRange(List<Sweet> sweets, int firstRangeValue, int lastRangeValue)
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

        public List<Sweet> GetSweetsByAlcoholRange(List<Sweet> sweets, int firstRangeValue, int lastRangeValue)
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

        public List<Sweet> GetSweetsByWeightRange(List<Sweet> sweets, int firstRangeValue, int lastRangeValue)
        {
            sweets = sweets.Where(s => s.Weight >= firstRangeValue && s.Weight <= lastRangeValue).Select(s => s).ToList();

            return sweets;
        }

        public List<Sweet> GetSweetsByKkalRange(List<Sweet> sweets, int firstRangeValue, int lastRangeValue)
        {
            sweets = sweets.Where(s => s.Kkal >= firstRangeValue && s.Kkal <= lastRangeValue).Select(s => s).ToList();

            return sweets;
        }

        public Gift MakeGift(List<Sweet> sweets, Presentee presentee)
        {
            Gift makedGift = new Gift();

            makedGift.Sweets = sweets;

            sweets.ForEach(s => makedGift.Weight += s.Weight);

            sweets.ForEach(s => makedGift.Kkal += s.Kkal);

            makedGift.CountOfSweets = sweets.Count();

            makedGift.BelongToPresentee = presentee;

            return makedGift;
        }
    }
}
