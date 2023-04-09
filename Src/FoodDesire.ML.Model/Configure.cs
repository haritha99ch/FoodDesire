using FoodDesire.ML.Model.Services;

namespace FoodDesire.ML.Model;
public static class Configure {
    public static void ConfigureServices(IServiceCollection services) {
        services.AddScoped<IPredictionService, PredictionService>();
    }
}
