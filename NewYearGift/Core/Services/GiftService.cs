using NewYearGift.App.Constants;
using NewYearGift.DAL.Models.Sweets;
using NewYearGift.Models.Gifts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewYearGift.Core.Services
{
    public static class GiftService
    {
        public static int CalculateGiftWeight(Gift gift)
        {
            int weight = 0;

            foreach (var sweet in gift.Sweets)
            {
                weight += sweet.Weight;
            }

            return weight;
        }

        public static int CheckAndConvertInputToInt(string input)
        {
            int result;

            if (int.TryParse(input, out result))
            {
                return result;
            }
            else
            {
                result = -1;
                return result;
            }

        }

        public static List<Sweet> SortSweetsByName(List<Sweet> sweets, SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                sweets = sweets.OrderBy(s => s.Name).ToList();
            }
            else
            {
                sweets = sweets.OrderByDescending(s => s.Name).ToList();
            }

            return sweets;
        }

        public static List<Sweet> SortSweetsByWeight(List<Sweet> sweets, SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                sweets = sweets.OrderBy(s => s.Weight).ToList();
            }
            else
            {
                sweets = sweets.OrderByDescending(s => s.Weight).ToList();
            }

            return sweets;
        }

        public static List<Sweet> SortSweetsByKkal(List<Sweet> sweets, SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                sweets = sweets.OrderBy(s => s.Kkal).ToList();
            }
            else
            {
                sweets = sweets.OrderByDescending(s => s.Kkal).ToList();
            }

            return sweets;
        }

        public static List<Sweet> GetSweetsByPresentee(List<Sweet> sweets, Presentee presentee)
        {
            List<Sweet> resultSweets = new List<Sweet>();
            foreach (Sweet sweet in sweets)
            {
                if (presentee == Presentee.Children)
                {
                    if ((sweet is SugarSweet) || (sweet is SugarFreeSweet))
                    {
                        resultSweets.Add(sweet);
                    }
                }
                else
                {
                    resultSweets = sweets;
                }
            }

            return resultSweets;
        }

        public static List<Sweet> GetSweetsByIndexRange(List<Sweet> sweets, string[] indexRange)
        {
            List<Sweet> resultSweets = new List<Sweet>();

            foreach (var ch in indexRange)
            {
                resultSweets.Add(sweets.ElementAt(Convert.ToInt32(ch) - 1));
            }

            return resultSweets;
        }

        public static List<Sweet> SortSweetsByShape(List<Sweet> sweets, SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                sweets = sweets.OrderBy(s => s.Shape.Name).ToList();
            }
            else
            {
                sweets = sweets.OrderByDescending(s => s.Shape.Name).ToList();
            }

            return sweets;
        }

        public static List<Sweet> SortSweetsByFilling(List<Sweet> sweets, SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                sweets = sweets.OrderBy(s => s.Filling.Name).ToList();
            }
            else
            {
                sweets = sweets.OrderByDescending(s => s.Filling.Name).ToList();
            }

            return sweets;
        }

        public static List<Sweet> FindSweetsBySugarRange(List<Sweet> sweets, int firstRangeValue, int lastRangeValue)
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

        public static List<Sweet> FindSweetsByAlcoholRange(List<Sweet> sweets, int firstRangeValue, int lastRangeValue)
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
                sweetsResult.Add(alcoholSweet as Sweet);
            }

            foreach (AlcoholicSugarSweet alcoholSweet in alcoholSugarSweets)
            {
                sweetsResult.Add(alcoholSweet as Sweet);
            }

            return sweetsResult;
        }

        public static List<Sweet> FindSweetsByWeightRange(List<Sweet> sweets, int firstRangeValue, int lastRangeValue)
        {
            sweets = sweets.Where(s => s.Weight >= firstRangeValue && s.Weight <= lastRangeValue).Select(s => s).ToList();

            return sweets;
        }

        public static List<Sweet> FindSweetsByKkalRange(List<Sweet> sweets, int firstRangeValue, int lastRangeValue)
        {
            sweets = sweets.Where(s => s.Kkal >= firstRangeValue && s.Kkal <= lastRangeValue).Select(s => s).ToList();

            return sweets;
        }

        public static Gift MakeGift(List<Sweet> sweets, Presentee belongTo)
        {
            Gift newGift = new Gift();

            newGift.Sweets = sweets;

            sweets.ForEach(s => newGift.Weight += s.Weight);
            sweets.ForEach(s => newGift.Kkal += s.Kkal);

            newGift.CountOfSweets = sweets.Count();

            newGift.BelongToPresentee = belongTo;

            return newGift;
        }
    }
}
