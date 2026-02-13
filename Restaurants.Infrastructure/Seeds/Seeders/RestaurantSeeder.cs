using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.ValueObjects;
using Restaurants.Infrastructure.Persistance.Seeds.Abstractions;
using Restaurants.Infrastructure.Persistance.Seeds.Seeders.Selectors;

namespace Restaurants.Infrastructure.Persistance.Seeds.Seeders;

public class RestaurantSeeder(RestaurantsDbContext context) : IEntitySeeder
{

    public async Task SeedAsync()
    {
        if (await context.Restaurants.AnyAsync())
            return;
        var categories = await context.Categories
            .Select(c => new CategorySelector(c.Id, c.Name))
            .ToListAsync();
        await context.Restaurants.AddRangeAsync(GetRestaurants(categories));

        await context.SaveChangesAsync();
    }

    private IEnumerable<Restaurant> GetRestaurants(List<CategorySelector> categories)
    {
        var fastFood = categories.Single(c => c.Name == "Fast Food");
        var italian = categories.Single(c => c.Name == "Italian");
        var japanese = categories.Single(c => c.Name == "Japanese");
        var chinese = categories.Single(c => c.Name == "Chinese");
        var indian = categories.Single(c => c.Name == "Indian");

        return new List<Restaurant>
        {
            new()
            {
                Name = "KFC",
                CategoryId = fastFood.Id,
                Description = "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                ContactEmail = "contact@kfc.com",
                ContactNumber = "+44 20 1234 5678",
                HasDelivery = true,
                Address = new Address
                {
                    City = "London",
                    Street = "Cork St 5",
                    PostalCode = "WC2N 5DU"
                }
            },
            new()
            {
                Name = "McDonald's",
                CategoryId = fastFood.Id,
                Description = "McDonald's Corporation operates and franchises McDonald's restaurants worldwide.",
                ContactEmail = "contact@mcdonalds.com",
                ContactNumber = "+44 20 8765 4321",
                HasDelivery = true,
                Address = new Address
                {
                    City = "London",
                    Street = "Boots 193",
                    PostalCode = "W1F 8SR"
                }
            },
            new()
            {
                Name = "Pizza Hut",
                CategoryId = italian.Id,
                Description = "Pizza Hut is an American multinational restaurant chain known for its Italian-American cuisine.",
                ContactEmail = "contact@pizzahut.com",
                ContactNumber = "+44 20 2345 6789",
                HasDelivery = true,
                Address = new Address
                {
                    City = "Manchester",
                    Street = "Market St 45",
                    PostalCode = "M1 1WR"
                }
            },
            new()
            {
                Name = "Sushi Daily",
                CategoryId = japanese.Id,
                Description = "Authentic Japanese sushi and sashimi made fresh daily with premium ingredients.",
                ContactEmail = "hello@sushidaily.com",
                ContactNumber = "+44 20 3456 7890",
                HasDelivery = true,
                Address = new Address
                {
                    City = "Birmingham",
                    Street = "New St 78",
                    PostalCode = "B2 4QX"
                }
            },
            new()
            {
                Name = "Wok & Go",
                CategoryId = chinese.Id,
                Description = "Quick and delicious Chinese street food, wok-tossed to perfection.",
                ContactEmail = "info@wokandgo.com",
                ContactNumber = "+44 20 4567 8901",
                HasDelivery = true,
                Address = new Address
                {
                    City = "Leeds",
                    Street = "Briggate 92",
                    PostalCode = "LS1 6NP"
                }
            },
            new()
            {
                Name = "Taj Mahal",
                CategoryId = indian.Id,
                Description = "Authentic Indian cuisine with traditional clay oven cooking and aromatic spices.",
                ContactEmail = "booking@tajmahal.com",
                ContactNumber = "+44 20 5678 9012",
                HasDelivery = true,
                Address = new Address
                {
                    City = "Liverpool",
                    Street = "Bold St 56",
                    PostalCode = "L1 4DS"
                }
            }
        };
    }
}
