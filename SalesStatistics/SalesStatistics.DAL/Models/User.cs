using Microsoft.AspNetCore.Identity;

namespace SalesStatistics.DAL.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
