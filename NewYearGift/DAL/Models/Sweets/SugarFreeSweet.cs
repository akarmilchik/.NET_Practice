using NewYearGift.DAL.Models.Sweets.Parameters;

namespace NewYearGift.DAL.Models.Sweets
{

    class SugarFreeSweet : Sweet
    {
        public SugarFreeSweet(string name, int weight, int kkal, Filling filling, Shape shape)
            : base(name, weight, kkal, filling, shape)
        {

        }
    }
}
