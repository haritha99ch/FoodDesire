using FoodDesire.AppSettings.Helpers;
using Microsoft.Extensions.Configuration;

namespace FoodDesire.AppSettings;
public static class Configure {
    public static void ConfigureEnvironment(IConfigurationBuilder config, string environmentName) {
        try {
            config.AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true);
        } catch {
            throw new Exception($"Configure FoodDesire.AppSettings/appsettings.{environmentName}.json.");
        }
        config.AddEnvironmentVariables();

        IConfigurationRoot? _config = config.Build();
        AppSettingsValidator.Validate(_config);
        _config = null;
    }
}
