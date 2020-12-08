using NewYearGift.App.Constants;
using System.Collections.Generic;

namespace NewYearGift.DAL.Models.Sweets
{
    abstract class Sweet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Kkal { get; set; }
        public List<SweetParameter> SweetParameters { get; set; }
    }
}
