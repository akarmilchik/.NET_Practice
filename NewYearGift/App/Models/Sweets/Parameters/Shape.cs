namespace NewYearGift.App.Models.Sweets.Parameters
{
    public class Shape
    {
        public string Name { get; set; }
        public int NumberOfCorners { get; set; }

        public override string ToString()
        {
            return $"Shape: {Name}, Corners:{NumberOfCorners}";
        }
    }
}
