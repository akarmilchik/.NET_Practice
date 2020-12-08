using System.Collections.Generic;

namespace NewYearGift.DAL.Models.Sweets
{

    // Can use for add more patameters for sweets, example shape or filling
    public abstract class SweetParameter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Variants { get; set; }
    }
}
