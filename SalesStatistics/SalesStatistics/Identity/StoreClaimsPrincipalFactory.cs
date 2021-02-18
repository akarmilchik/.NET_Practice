using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SalesStatistics.DAL.Models;

namespace SalesStatistics.Identity
{
    public class StoreClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
    {
        public StoreClaimsPrincipalFactory(UserManager<User> userManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            return identity;
        }
    }
}
