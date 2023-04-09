using FoodDesire.ML.Model.Helpers;

Console.WriteLine("Configuring app...");

var Host = Microsoft.Extensions.Hosting.Host
    .CreateDefaultBuilder()
    .UseContentRoot(AppContext.BaseDirectory)
    .ConfigureAppConfiguration(AppConfigurator.Configure)
    .ConfigureServices(AppConfigurator.Configure)
    .Build();

Console.WriteLine("Configuring Model...");
IRecommendationService? service = Host.Services.GetRequiredService<IRecommendationService>();
try {
    await service.ConfigurePredictionEngine();
    Console.WriteLine("Model configured");

    Console.WriteLine("Model saved");
    service.SaveModel();

    // TODO: Evaluate model
} catch (Exception ex) {
    Console.WriteLine(ex.Message);
    Console.WriteLine("Stopping the configuration");
}
