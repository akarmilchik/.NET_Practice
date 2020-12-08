using NewYearGift.App.Constants;
using NewYearGift.DAL.Models.Sweets;
using NewYearGift.Models.Gifts;
using System.Collections.Generic;
using System.Linq;

namespace NewYearGift.DAL.Models
{
    class GiftMaker
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

        public List<Sweet> SortSweetsById(List<Sweet> sweets, SortOrder sortOrder)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                sweets = sweets.OrderBy(s => s.Id).ToList();
            }
            else
            {
                sweets = sweets.OrderByDescending(s => s.Id).ToList();
            }

            return sweets;
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

        public List<Sweet> SortSweetsByOtherParameter(List<Sweet> sweets, SortOrder sortOrder, SweetParameter sweetParameter)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                sweets = sweets.OrderBy(s => s.SweetParameters.Where(sp => sp.Id == sweetParameter.Id)).ToList();  
            }
            else
            {
                sweets = sweets.OrderByDescending(s => sweetParameter).ToList();
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
    }
}
