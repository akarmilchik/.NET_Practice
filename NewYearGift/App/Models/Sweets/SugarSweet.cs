using NewYearGift.DAL.Models.Interfaces;
using NewYearGift.DAL.Models.Sweets.Parameters;
using System.Collections.Generic;

namespace NewYearGift.DAL.Models.Sweets
{
    public class SugarSweet : Sweet, ISugarSweet
    {
        public int SugarWeight { get; private set; }
        
        public SugarSweet(string name, int weight, int kkal, Filling filling, Shape shape, int sugarWeight) : base(name, weight, kkal, filling, shape)
        {
            this.SugarWeight = sugarWeight;
        }

        public override Dictionary<string, string> ShowData()
        {
            return new Dictionary<string, string>
            {
                {"Name", this.Name},
                { "Weight", this.Weight.ToString() },
                { "Kkal", this.Kkal.ToString() },
                { "Filling", this.Filling.Name },
                { "Shape", this.Shape.Name },
                { "SugarWeight", this.SugarWeight.ToString() }
            };
        }
    }
}
