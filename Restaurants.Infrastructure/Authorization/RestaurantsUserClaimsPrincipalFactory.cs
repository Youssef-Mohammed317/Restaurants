using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;
using System.Security.Claims;

namespace Restaurants.Infrastructure.Authorization;

public class RestaurantsUserClaimsPrincipalFactory(UserManager<User> userManager, IOptions<IdentityOptions> optionsAccessor) : UserClaimsPrincipalFactory<User>(userManager, optionsAccessor)
{
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var identity = await GenerateClaimsAsync(user);
        if (user.Nationality != null)
        {
            identity.AddClaim(new Claim("Nationality", user.Nationality));
        }
        if (user.DateOfBirth != null)
        {
            identity.AddClaim(new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd")));
        }

        return new ClaimsPrincipal(identity);
    }
}
