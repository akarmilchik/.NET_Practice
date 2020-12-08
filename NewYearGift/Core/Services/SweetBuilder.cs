using NewYearGift.DAL.Models.Sweets;
using System.Collections.Generic;


namespace NewYearGift.Core.Services
{
    public abstract class SweetBuilder
    {
        public Sweet Sweet { get; set; }

        public abstract void BuildName();
        public abstract void BuildWeight();
        public abstract void Kkal();
        public abstract void BuildSweetParameters();
    }
}
