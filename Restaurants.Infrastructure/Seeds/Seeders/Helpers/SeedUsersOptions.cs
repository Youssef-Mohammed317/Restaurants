namespace Restaurants.Infrastructure.Seeds.Seeders.Helpers;

public class SeedUsersOptions
{
    public SeedUser Admin { get; set; } = new();
    public SeedUser Owner { get; set; } = new();
    public SeedUser User { get; set; } = new();
}
