using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistance.Seeds.Abstractions;
using Restaurants.Infrastructure.Seeds.Seeders.Helpers;

namespace Restaurants.Infrastructure.Persistance.Seeds.Seeders;

public class DishSeeder(RestaurantsDbContext context) : IEntitySeeder
{

    public async Task SeedAsync()
    {
        if (await context.Dishes.AnyAsync())
            return;
        var restaurants = await context.Restaurants
            .Select(r => new RestaurantSelector(r.Id, r.Name))
            .ToListAsync();
        await context.Dishes.AddRangeAsync(GetDishes(restaurants));

        await context.SaveChangesAsync();
    }

    private IEnumerable<Dish> GetDishes(List<RestaurantSelector> restaurants)
    {
        var kfc = restaurants.Single(r => r.Name == "KFC");
        var mcDonalds = restaurants.Single(r => r.Name == "McDonald's");
        var pizzaHut = restaurants.Single(r => r.Name == "Pizza Hut");
        var sushiDaily = restaurants.Single(r => r.Name == "Sushi Daily");
        var wokGo = restaurants.Single(r => r.Name == "Wok & Go");
        var tajMahal = restaurants.Single(r => r.Name == "Taj Mahal");

        return new List<Dish>
    {
        // KFC
        new() { Name = "Nashville Hot Chicken", Description = "Nashville Hot Chicken (10 pcs.)", Price = 10.30M, KiloCalories = 1250, RestaurantId = kfc.Id },
        new() { Name = "Chicken Nuggets", Description = "Chicken Nuggets (5 pcs.)", Price = 5.30M, KiloCalories = 450, RestaurantId = kfc.Id },
        new() { Name = "Zinger Burger", Description = "Spicy crispy chicken burger with lettuce and mayo", Price = 6.99M, KiloCalories = 630, RestaurantId = kfc.Id },
        new() { Name = "Popcorn Chicken", Description = "Bite-sized chicken pieces (Regular)", Price = 4.99M, KiloCalories = 380, RestaurantId = kfc.Id },

        // McDonald's
        new() { Name = "Big Mac", Description = "Two beef patties, special sauce, lettuce, cheese, pickles, onions on a sesame seed bun", Price = 4.99M, KiloCalories = 540, RestaurantId = mcDonalds.Id },
        new() { Name = "McChicken", Description = "Crispy chicken patty with lettuce and mayo", Price = 3.99M, KiloCalories = 410, RestaurantId = mcDonalds.Id },
        new() { Name = "French Fries", Description = "World famous golden fries (Medium)", Price = 2.49M, KiloCalories = 380, RestaurantId = mcDonalds.Id },
        new() { Name = "McFlurry", Description = "Soft ice cream with Oreo cookies", Price = 2.99M, KiloCalories = 510, RestaurantId = mcDonalds.Id },

        // Pizza Hut
        new() { Name = "Margherita Pizza", Description = "Classic tomato sauce, mozzarella, and basil (Medium)", Price = 12.99M, KiloCalories = 850, RestaurantId = pizzaHut.Id },
        new() { Name = "Pepperoni Pizza", Description = "Tomato sauce, mozzarella, and pepperoni (Medium)", Price = 14.99M, KiloCalories = 980, RestaurantId = pizzaHut.Id },
        new() { Name = "Garlic Bread", Description = "Crispy bread with garlic butter", Price = 4.49M, KiloCalories = 320, RestaurantId = pizzaHut.Id },
        new() { Name = "Chicken Wings", Description = "BBQ glazed chicken wings (6 pcs.)", Price = 7.99M, KiloCalories = 560, RestaurantId = pizzaHut.Id },

        // Sushi Daily
        new() { Name = "California Roll", Description = "Crab stick, avocado, and cucumber (8 pcs.)", Price = 8.99M, KiloCalories = 420, RestaurantId = sushiDaily.Id },
        new() { Name = "Salmon Nigiri", Description = "Fresh salmon over pressed vinegared rice (2 pcs.)", Price = 5.99M, KiloCalories = 180, RestaurantId = sushiDaily.Id },
        new() { Name = "Miso Soup", Description = "Traditional Japanese soup with tofu and seaweed", Price = 2.99M, KiloCalories = 90, RestaurantId = sushiDaily.Id },
        new() { Name = "Rainbow Roll", Description = "Assorted fish on California roll (8 pcs.)", Price = 12.99M, KiloCalories = 550, RestaurantId = sushiDaily.Id },

        // Wok & Go
        new() { Name = "Kung Pao Chicken", Description = "Spicy diced chicken with peanuts and vegetables", Price = 11.99M, KiloCalories = 720, RestaurantId = wokGo.Id },
        new() { Name = "Spring Rolls", Description = "Crispy rolls with vegetables (3 pcs.)", Price = 4.50M, KiloCalories = 290, RestaurantId = wokGo.Id },
        new() { Name = "Fried Rice", Description = "Yangzhou style fried rice with BBQ pork", Price = 8.99M, KiloCalories = 680, RestaurantId = wokGo.Id },
        new() { Name = "Dim Sum Platter", Description = "Assorted steamed dumplings (6 pcs.)", Price = 9.99M, KiloCalories = 410, RestaurantId = wokGo.Id },

        // Taj Mahal
        new() { Name = "Chicken Tikka Masala", Description = "Grilled chicken in creamy tomato curry", Price = 13.99M, KiloCalories = 890, RestaurantId = tajMahal.Id },
        new() { Name = "Garlic Naan", Description = "Traditional Indian flatbread with garlic", Price = 3.49M, KiloCalories = 260, RestaurantId = tajMahal.Id },
        new() { Name = "Biryani", Description = "Fragrant basmati rice with spiced chicken", Price = 12.99M, KiloCalories = 780, RestaurantId = tajMahal.Id },
        new() { Name = "Samosas", Description = "Crispy pastry filled with spiced potatoes and peas (2 pcs.)", Price = 4.99M, KiloCalories = 350, RestaurantId = tajMahal.Id },
    };
    }

}
