namespace NewYearGift.DAL.Models.Sweets.Parameters
{
    public class Filling
    {
        public string Name { get; set; }

        public string Solid { get; set; }

        public override string ToString()
        {
            return Solid + Name;
        }
    }
}
