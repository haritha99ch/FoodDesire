using FoodDesire.AppSettings.Helpers;
using Microsoft.Extensions.Configuration;

namespace FoodDesire.AppSettings;
public static class Configure {
    public static void ConfigureEnvironment(IConfigurationBuilder config) {
        config.AddUserSecrets<Program>();
        AppSettingsValidator.Validate(config);
    }
}
