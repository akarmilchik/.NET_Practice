using NewYearGift.App.Constants;
using NewYearGift.DAL.Models.Sweets;
using NewYearGift.Models.Gifts;
using System.Collections.Generic;
using System.Linq;

namespace NewYearGift.DAL.Models
{
    public class GiftMaker
    {
        public int CalculateGiftWeight(Gift gift)
        {
            int weight = 0;

            foreach (var sweet in gift.Sweets)
            {
               weight += sweet.Weight;
            }

            return weight;
        }

        public List<Sweet> SortSweetsByName(ref List<Sweet> sweets, SortOrder sortOrder)
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


        public List<Sweet> SortSweetsByWeight(List<Sweet> sweets, SortOrder sortOrder)
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

        public List<Sweet> SortSweetsByShape(List<Sweet> sweets, SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                sweets = sweets.OrderBy(s => s.Shape).ToList();
            }
            else
            {
                sweets = sweets.OrderByDescending(s => s.Shape).ToList();
            }

            return sweets;
        }


        public List<Sweet> SortSweetsByFilling(List<Sweet> sweets, SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                sweets = sweets.OrderBy(s => s.Filling).ToList();
            }
            else
            {
                sweets = sweets.OrderByDescending(s => s.Filling).ToList();
            }

            return sweets;
        }

        public List<SugarSweet> FindSugarSweetsFromAllSweets(List<Sweet> sweets)
        {
            var sugarSweets = sweets.Where(s => s is SugarSweet).Select(s => s as SugarSweet).ToList();

            return sugarSweets;
        }


        public List<SugarSweet> FindSweetsBySugarRange(List<SugarSweet> sweets, int firstRangeValue, int lastRangeValue)
        {
            sweets = sweets.Where(s => s.SugarWeight >= firstRangeValue && s.SugarWeight <= lastRangeValue).Select(s => s).ToList();

            return sweets;
        }

        public Gift MakeGiftFromSweets(List<Sweet> sweets, Presentee belongTo)
        {
            Gift newGift = new Gift();

            newGift.Sweets = sweets;

            sweets.ForEach(s => newGift.Weight += s.Weight);

            newGift.CountOfSweets = sweets.Count();

            newGift.BelongToPresentee = belongTo;

            return newGift;
        }
    }
}
