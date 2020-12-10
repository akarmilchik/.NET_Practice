using NewYearGift.DAL.Models.Interfaces;
using NewYearGift.DAL.Models.Sweets.Parameters;
using System.Collections.Generic;

namespace NewYearGift.DAL.Models.Sweets
{
    public class AlcoholicSugarSweet : Sweet, IAlcoholicSweet, ISugarSweet
    {
        public int SugarWeight { get; private set; }
        public int AlcoholDegree { get; private set; }
        
        public AlcoholicSugarSweet(string name, int weight, int kkal, Filling filling, Shape shape, int sugarWeight, int alcoholDegree)
            : base(name, weight, kkal, filling, shape)
        {
            this.SugarWeight = sugarWeight;
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
                { "SugarWeight", this.SugarWeight.ToString() },
                { "AlcoholDegree", this.AlcoholDegree.ToString() }
            };
        }
    }
}
