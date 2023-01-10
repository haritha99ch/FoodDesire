using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FoodDesire.Core.Test;
public static class DbContextHelper {
    private static readonly IgnoringIdentityResolutionInterceptor IgnoringIdentityResolutionInterceptor = new();

    public static FoodDesireContext GetContext(string name) {
        DbContextOptions<FoodDesireContext>? dbOptions = new DbContextOptionsBuilder<FoodDesireContext>()
                .AddInterceptors(IgnoringIdentityResolutionInterceptor)
                .UseInMemoryDatabase(name).Options;
        return new FoodDesireContext(dbOptions);
    }
}
