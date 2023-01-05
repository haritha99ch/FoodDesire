using Microsoft.EntityFrameworkCore;

namespace FoodDesire.DAL.Test;
public static class DbContextHelper {
    public static FoodDesireContext GetContext(string name) {
        DbContextOptions<FoodDesireContext>? dbOptions = new DbContextOptionsBuilder<FoodDesireContext>()
             .UseInMemoryDatabase(name).Options;
        return new FoodDesireContext(dbOptions);
    }
}
