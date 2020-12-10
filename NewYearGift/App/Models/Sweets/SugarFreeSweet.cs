using NewYearGift.DAL.Models.Sweets.Parameters;
using System.Collections.Generic;

namespace NewYearGift.DAL.Models.Sweets
{

    class SugarFreeSweet : Sweet
    {
        public SugarFreeSweet(string name, int weight, int kkal, Filling filling, Shape shape)
            : base(name, weight, kkal, filling, shape)
        {

        }

        public override Dictionary<string, string> ShowData()
        {
            return new Dictionary<string, string>
            {
                {"Name", this.Name},
                { "Weight", this.Weight.ToString() },
                { "Kkal", this.Kkal.ToString() },
                { "Filling", this.Filling.Name },
                { "Shape", this.Shape.Name }
            };
        }
    }
}
