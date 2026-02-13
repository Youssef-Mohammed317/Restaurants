using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistance.Seeds.Abstractions;
using Restaurants.Infrastructure.Seeds.Seeders.Helpers;

namespace Restaurants.Infrastructure.Persistance.Seeds.Seeders;

public class UserSeeder(
    UserManager<User> userManager,
    IOptions<SeedUsersOptions> options
) : IEntitySeeder
{
    public async Task SeedAsync()
    {
        await EnsureUserWithRole(options.Value.Admin, UserRoles.Admin);
        await EnsureUserWithRole(options.Value.Owner, UserRoles.Owner);
        await EnsureUserWithRole(options.Value.User, UserRoles.User);
    }

    private async Task EnsureUserWithRole(SeedUser seedUser, string roleName)
    {
        var existing = await userManager.FindByEmailAsync(seedUser.Email);
        if (existing is null)
        {
            var user = new User
            {
                Email = seedUser.Email,
                UserName = seedUser.UserName,
                EmailConfirmed = true
            };

            var createResult = await userManager.CreateAsync(user, seedUser.Password);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to create {seedUser.Email}: {errors}");
            }

            existing = user;
        }

        if (!await userManager.IsInRoleAsync(existing, roleName))
        {
            var addRoleResult = await userManager.AddToRoleAsync(existing, roleName);
            if (!addRoleResult.Succeeded)
            {
                var errors = string.Join(", ", addRoleResult.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to assign {seedUser.Email} with role {roleName}: {errors}");
            }
        }
    }
}