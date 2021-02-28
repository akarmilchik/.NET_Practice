namespace SalesStatistics.Core.Constants
{
    public static class CollectionsParams
    {
        public static readonly string[] Ages = new string[5] { "20-60","20-30", "30-40", "40-50", "50-60" };
        public static readonly string[] Weights = new string[6] { "0-1000", "0-100", "100-500", "500-1000", "1000-5000", "5000-10000" };
        public static readonly string[] Costs = new string[7] { "0-500", "0-20", "20-40", "40-60", "60-100", "100-300", "300-500" };
        public static readonly int[] PageSizes = new int[7] { 4, 6, 8, 10, 12, 14, 16 };
    }
}