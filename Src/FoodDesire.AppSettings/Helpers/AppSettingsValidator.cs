using Microsoft.Extensions.Configuration;

namespace FoodDesire.AppSettings.Helpers;
public static class AppSettingsValidator {
    public static void Validate(IConfiguration config) {
        bool invalidAppSettings = false;
        List<string> errors = new();
        if (config.GetConnectionString("DefaultConnection") == null) {
            invalidAppSettings = true;
            errors.Add("Missing ConnectionStrings:DefaultConnection");
        }

        if (config.GetConnectionString("StorageConnection") == null) {
            invalidAppSettings = true;
            errors.Add("Missing ConnectionStrings:StorageConnection");
        }

        if (config["ClientId"] == null) {
            invalidAppSettings = true;
            errors.Add("Missing ConnectionStrings:ClientId");
        }

        if (config.GetSection("JWT")["SignInKey"] == null) {
            invalidAppSettings = true;
            errors.Add("Missing JWT:SignInKey");
        }

        if (!invalidAppSettings) return;
        string environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";
        string message = $"Missing elements in FoodDesire.AppSettings/appsettings.{environmentName}.json.\n";
        message = $"{message}{string.Join("\n", errors)}";
        throw new Exception(message);
    }
}
