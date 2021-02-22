namespace SalesStatistics.DAL.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}