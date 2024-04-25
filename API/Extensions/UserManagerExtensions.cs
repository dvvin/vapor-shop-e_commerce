using System.Security.Claims;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindByUserByClaimsPrincipleWithAddressAsync(
            this UserManager<AppUser> Input,
            ClaimsPrincipal User
        )
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            return await Input
                .Users.Include(x => x.Address)
                .SingleOrDefaultAsync(x => x.Email == email);
        }

        public static async Task<AppUser> FindByEmailFromClaimsPrinciple(
            this UserManager<AppUser> Input,
            ClaimsPrincipal User
        )
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            return await Input.Users.SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}
