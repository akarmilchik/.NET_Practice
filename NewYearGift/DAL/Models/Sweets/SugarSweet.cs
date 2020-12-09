using NewYearGift.DAL.Models.Sweets.Parameters;

namespace NewYearGift.DAL.Models.Sweets
{
    public class SugarSweet : Sweet
    {
        public int SugarWeight { get; private set; }
        
        public SugarSweet(string name, int weight, int kkal, Filling filling, Shape shape, int sugarWeight) : base(name, weight, kkal, filling, shape)
        {
            this.SugarWeight = sugarWeight;
        }
    }
}
