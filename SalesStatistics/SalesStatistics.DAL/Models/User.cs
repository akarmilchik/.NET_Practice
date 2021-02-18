using Microsoft.AspNetCore.Identity;

namespace SalesStatistics.DAL.Models
{
    public class User : IdentityUser
    {
        public override string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
