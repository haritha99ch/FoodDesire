using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FoodDesire.DAL.Test;
public static class DbContextHelper {
    private static readonly IgnoringIdentityResolutionInterceptor IgnoringIdentityResolutionInterceptor = new();

    public static ApplicationDbContext GetContext(string name) {
        DbContextOptions<ApplicationDbContext>? dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .AddInterceptors(IgnoringIdentityResolutionInterceptor)
                .UseInMemoryDatabase(name).Options;
        return new ApplicationDbContext(dbOptions);
    }
}
