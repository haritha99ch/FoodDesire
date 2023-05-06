using Microsoft.Extensions.Azure;

namespace FoodDesire.ML.Model.Helpers;
internal static class AppConfigurator {
    internal static void Configure(HostBuilderContext context, IConfigurationBuilder config) {
        string environmentName = context.HostingEnvironment.EnvironmentName;
        AppSettings.Configure.ConfigureEnvironment(config, environmentName);
    }

    internal static void Configure(HostBuilderContext context, IServiceCollection services) {

        // Core Services
        string connectionString = context.Configuration.GetConnectionString("DefaultConnection")!;
        DAL.Configure.ConfigureServices(services, connectionString);

        // Azure Storage
        string storageConnectionString = context.Configuration.GetConnectionString("StorageConnection")!;
        services.AddAzureClients(builder => builder.AddBlobServiceClient(storageConnectionString));

        // AutoMapper
        MapperConfiguration? configuration = new(DtoConfigurator.Configure);
        IMapper? mapper = configuration.CreateMapper();
        services.AddSingleton(mapper);

        //ML Services
        services.AddTransient<IRecommendationService, RecommendationService>();
    }
}
