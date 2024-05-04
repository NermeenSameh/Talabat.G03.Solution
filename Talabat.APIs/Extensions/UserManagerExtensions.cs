using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Entities.Identity;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Talabat.APIs.Extensions
{
	public static class UserManagerExtensions
	{
		public static async Task<ApplicationUser?> FindUserWithAddressByEmailAsync(this UserManager<ApplicationUser> userManager,ClaimsPrincipal User)
		{
			var email = User.FindFirstValue(ClaimTypes.Email);

			var user = await userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.NormalizedEmail == email.ToUpper());
			
			return user;
		}

	}
}
