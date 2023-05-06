using Microsoft.Extensions.Azure;

namespace FoodDesire.ML.Model;
public static class Configure {
    public static void ConfigureServices(IServiceCollection services) {
        services.AddSingleton<IPredictionService, PredictionService>();

        // Azure Storage
        IConfiguration? configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        string storageConnectionString = configuration.GetConnectionString("StorageConnection")!;
        services.AddAzureClients(builder => builder.AddBlobServiceClient(storageConnectionString));

    }
}
