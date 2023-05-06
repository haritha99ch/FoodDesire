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

    Console.WriteLine("Model saving...");
    await service.SaveModel();
    Console.WriteLine("Model Saved");

} catch (Exception ex) {
    Console.WriteLine(ex.Message);
    Console.WriteLine("Stopping the configuration");
}
