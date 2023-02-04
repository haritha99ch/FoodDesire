using Microsoft.EntityFrameworkCore;

namespace FoodDesire.Core.Test.Helpers;
public static class DbContextHelper {
    public static FoodDesireContext GetContext(string name) {
        DbContextOptions<FoodDesireContext>? dbOptions = new DbContextOptionsBuilder<FoodDesireContext>()
                .UseInMemoryDatabase(name).Options;
        return new FoodDesireContext(dbOptions);
    }

    public static void ConfigureDbContextOptions(string name, DbContextOptionsBuilder options) {
        options.UseInMemoryDatabase(name);
    }
}