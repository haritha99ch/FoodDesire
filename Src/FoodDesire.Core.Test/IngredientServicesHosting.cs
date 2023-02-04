namespace FoodDesire.Core.Test;
[TestFixture]
public class IngredientServicesHosting {
    private IngredientServices _testRunner;

    public IngredientServicesHosting() {
        var hostBuilder = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) => {
                services.AddDbContext<FoodDesireContext>(options => {
                    DbContextHelper.ConfigureDbContextOptions("IngredientServices", options);
                });
                DAL.Configure.ConfigureAllForTesting(services);
                Core.Configure.ConfigureAllForTesting(services);
                services.AddTransient<IngredientServices>();
            });
        var host = hostBuilder.Build();
        _testRunner = host.Services.GetRequiredService<IngredientServices>();
    }


    [Test, Order(1)]
    public async Task NewIngredientCategory() => await _testRunner.NewIngredientCategory();
}
