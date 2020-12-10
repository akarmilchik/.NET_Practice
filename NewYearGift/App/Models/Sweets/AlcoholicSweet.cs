using NewYearGift.DAL.Models.Interfaces;
using NewYearGift.DAL.Models.Sweets.Parameters;
using System.Collections.Generic;

namespace NewYearGift.DAL.Models.Sweets
{
    public class AlcoholicSweet : Sweet, IAlcoholicSweet
    {
        public int AlcoholDegree { get; private set; }
        
        public AlcoholicSweet(string name, int weight, int kkal, Filling filling, Shape shape, int alcoholDegree)
            : base(name, weight, kkal, filling, shape)
        {
            this.AlcoholDegree = alcoholDegree;
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
                { "AlcoholDegree", this.AlcoholDegree.ToString() }
            };
        }
    }
}
