namespace NewYearGift.App.Models.Sweets.Parameters
{
    public class Filling
    {
        public string Name { get; set; }
        public string Consistency { get; set; }

        public override string ToString()
        {
            return $"Filling: {Name}, Consistency: {Consistency}";
        }
    }
}
